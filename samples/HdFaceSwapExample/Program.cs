using FeralTic.DX11;
using FeralTic.DX11.Geometry;
using FeralTic.DX11.Resources;
using KGP;
using KGP.Direct3D11;
using KGP.Direct3D11.Buffers;
using KGP.Direct3D11.DataTables;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Processors;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultipleHdFaceTrackingSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RenderForm form = new RenderForm("Kinect multiple hd faces projected to rgb");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ProjectedTextureHdFaceView.fx", "VS_Simple");
            VertexShader vertexShaderIndexed = ShaderCompiler.CompileFromFile<VertexShader>(device, "ProjectedTextureHdFaceView.fx", "VS_Indexed");
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ProjectedTextureHdFaceView.fx", "PS");

            int maxFaceCount = Consts.MaxBodyCount;
            int faceVertexCount = (int)Microsoft.Kinect.Face.FaceModel.VertexCount;

            var vertRgbTempBuffer = new ColorSpacePoint[faceVertexCount];
            ColorSpacePoint[] facePoints = new ColorSpacePoint[faceVertexCount * maxFaceCount];

            DX11StructuredBuffer lookupBuffer = DX11StructuredBuffer.CreateDynamic<uint>(device, maxFaceCount);

            //Note : since in this case we use instancing, we only need a buffer for a single face
            HdFaceIndexBuffer faceIndexBuffer = new HdFaceIndexBuffer(device, 1);

            DynamicRgbSpaceFaceStructuredBuffer faceRgbBuffer = new DynamicRgbSpaceFaceStructuredBuffer(device, maxFaceCount);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            bool doQuit = false;
            bool invalidateFace = false;

            KinectSensorBodyFrameProvider provider = new KinectSensorBodyFrameProvider(sensor);
            BodyTrackingProcessor bodyTracker = new BodyTrackingProcessor();
            MultipleHdFaceProcessor multiFace = new MultipleHdFaceProcessor(sensor, bodyTracker, maxFaceCount);

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };

            bool uploadColor = false;
            ColorRGBAFrameData currentData = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { currentData = args.FrameData; uploadColor = true; };

            provider.FrameReceived += (sender, args) =>
            {
                bodyTracker.Next(args.FrameData);
            };

            multiFace.OnFrameResultsChanged += (sender, args) =>
            {
                invalidateFace = true;
            };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (invalidateFace)
                {
                    int offset = 0;
                    foreach (var data in multiFace.CurrentResults)
                    {
                        var vertices = data.FaceModel.CalculateVerticesForAlignment(data.FaceAlignment).ToArray();
                        sensor.CoordinateMapper.MapCameraPointsToColorSpace(vertices, vertRgbTempBuffer);
                        Array.Copy(vertRgbTempBuffer, 0, facePoints, offset, faceVertexCount);
                        offset += faceVertexCount;
                    }
                    faceRgbBuffer.Copy(context, facePoints, multiFace.CurrentResults.Count * faceVertexCount);
                    invalidateFace = false;
                }

                if (uploadColor)
                {
                    colorTexture.Copy(context, currentData);
                    uploadColor = false;
                }

                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);
                context.RenderTargetStack.Push(swapChain);

                context.Context.Rasterizer.State = device.RasterizerStates.BackCullSolid;
                context.Context.OutputMerger.BlendState = device.BlendStates.Disabled;
                device.Primitives.ApplyFullTri(context, colorTexture.ShaderView);
                device.Primitives.FullScreenTriangle.Draw(context);

                if (multiFace.CurrentResults.Count > 0)
                {
                    context.Context.VertexShader.SetShaderResource(0, faceRgbBuffer.ShaderView);
                    context.Context.PixelShader.SetSampler(0, device.SamplerStates.LinearClamp);
                    context.Context.PixelShader.SetShaderResource(0, colorTexture.ShaderView);

                    if (multiFace.CurrentResults.Count > 1)
                    {
                        uint[] buffer = new uint[multiFace.CurrentResults.Count];
                        for (uint i = 0; i < multiFace.CurrentResults.Count; i++)
                        {
                            buffer[i] = (uint)((i + 1) % multiFace.CurrentResults.Count);
                        }
                        lookupBuffer.WriteData(context, buffer);

                        context.Context.VertexShader.Set(vertexShaderIndexed);
                        context.Context.VertexShader.SetShaderResource(1, lookupBuffer.ShaderView);
                    }
                    else
                    {
                        context.Context.VertexShader.Set(vertexShader);
                    }

                    context.Context.PixelShader.Set(pixelShader);

                    //Attach index buffer, null topology since we fetch
                    faceIndexBuffer.AttachWithLayout(context);
                    faceIndexBuffer.DrawInstanced(context, multiFace.CurrentResults.Count);
                }

                context.RenderTargetStack.Pop();

                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            colorProvider.Dispose();
            colorTexture.Dispose();

            faceIndexBuffer.Dispose();
            faceRgbBuffer.Dispose();

            provider.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
            vertexShaderIndexed.Dispose();

            lookupBuffer.Dispose();


            sensor.Close();
        }
    }
}
