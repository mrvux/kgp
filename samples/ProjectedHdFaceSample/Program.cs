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

namespace ProjcectedHdFaceTrackingSample
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

            RenderForm form = new RenderForm("Kinect hd face projected to rgb");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ProjectedHdFaceView.fx", "VS");
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ProjectedHdFaceView.fx", "PS");


            HdFaceIndexBuffer faceIndexBuffer = new HdFaceIndexBuffer(device, 1);
            DynamicRgbSpaceFaceStructuredBuffer faceRgbBuffer = new DynamicRgbSpaceFaceStructuredBuffer(device, 1);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            bool doQuit = false;
            bool doUpload = false;

            KinectBody[] bodyFrame = null;
            KinectSensorBodyFrameProvider provider = new KinectSensorBodyFrameProvider(sensor);


            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };

            FaceModel currentFaceModel = new FaceModel();
            FaceAlignment currentFaceAlignment = new FaceAlignment();

            SingleHdFaceProcessor hdFaceProcessor = new SingleHdFaceProcessor(sensor);
            hdFaceProcessor.HdFrameReceived += (sender, args) => { currentFaceModel = args.FaceModel; currentFaceAlignment = args.FaceAlignment; doUpload = true; };

            bool uploadColor = false;
            ColorRGBAFrameData currentData = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { currentData = args.FrameData; uploadColor = true; };

            provider.FrameReceived += (sender, args) =>
            {
                bodyFrame = args.FrameData;
                var body = bodyFrame.TrackedOnly().ClosestBodies().FirstOrDefault();
                if (body != null)
                {
                    hdFaceProcessor.AssignBody(body);
                }
                else
                {
                    hdFaceProcessor.Suspend();
                }
            };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (doUpload)
                {
                    var vertices = currentFaceModel.CalculateVerticesForAlignment(currentFaceAlignment).ToArray();
                    var vertRgb = new ColorSpacePoint[vertices.Length];
                    sensor.CoordinateMapper.MapCameraPointsToColorSpace(vertices, vertRgb);

                    faceRgbBuffer.Copy(context, vertRgb);
                    doUpload = false;
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

                if (hdFaceProcessor.IsValid)
                {
                    context.Context.Rasterizer.State = device.RasterizerStates.WireFrame;
                    context.Context.OutputMerger.BlendState = device.BlendStates.AlphaBlend;
                    context.Context.VertexShader.SetShaderResource(0, faceRgbBuffer.ShaderView);

                    //Draw lines
                    context.Context.PixelShader.Set(pixelShader);
                    context.Context.VertexShader.Set(vertexShader);

                    //Attach index buffer, null topology since we fetch
                    faceIndexBuffer.AttachWithLayout(context);
                    faceIndexBuffer.Draw(context, 1);
                    
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


            sensor.Close();
        }
    }
}
