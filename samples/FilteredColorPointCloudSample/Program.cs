using FeralTic.DX11;
using FeralTic.DX11.Geometry;
using FeralTic.DX11.Resources;
using KGP;
using KGP.Direct3D11.Buffers;
using KGP.Direct3D11.DataTables;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
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

namespace JointColorSample
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cbCamera
    {
        public Matrix View;
        public Matrix Projection;
    }

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

            RenderForm form = new RenderForm("Kinect Simple filtered point cloud view sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            ComputeShader computeShader = ShaderCompiler.CompileFromFile<ComputeShader>(device, "ColoredPointCloudFilter.fx", "CS_Filter");

            VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ColoredPointCloudView.fx", "VS_Indirect");
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ColoredPointCloudView.fx", "PS");


            DX11NullGeometry nullGeom = new DX11NullGeometry(device);
            nullGeom.Topology = SharpDX.Direct3D.PrimitiveTopology.PointList;
            InstancedIndirectBuffer indirectDrawBuffer = new InstancedIndirectBuffer(device);


            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            cbCamera camera = new cbCamera();
            camera.Projection = Matrix.PerspectiveFovLH(1.57f * 0.5f, 1.3f, 0.01f, 100.0f);
            camera.View = Matrix.Translation(0.0f, 0.0f, 2.0f);

            camera.Projection.Transpose();
            camera.View.Transpose();

            ConstantBuffer<cbCamera> cameraBuffer = new ConstantBuffer<cbCamera>(device);
            cameraBuffer.Update(context, ref camera);

            bool doQuit = false;
            bool uploadCamera = false;
            bool uploadBodyIndex = false;
            bool uploadRgb = false;

            CameraRGBFrameData rgbFrame = new CameraRGBFrameData();
            DynamicCameraRGBTexture cameraTexture = new DynamicCameraRGBTexture(device);
            DepthToColorFrameData depthToColorFrame = new DepthToColorFrameData();
            DynamicDepthToColorTexture depthToColorTexture = new DynamicDepthToColorTexture(device);

            KinectSensorDepthFrameProvider provider = new KinectSensorDepthFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { rgbFrame.Update(sensor.CoordinateMapper, args.DepthData); depthToColorFrame.Update(sensor.CoordinateMapper, args.DepthData); uploadCamera = true; };

            BodyIndexFrameData bodyIndexFrame = null;
            DynamicBodyIndexTexture bodyIndexTexture = new DynamicBodyIndexTexture(device);
            KinectSensorBodyIndexFrameProvider bodyIndexProvider = new KinectSensorBodyIndexFrameProvider(sensor);
            bodyIndexProvider.FrameReceived += (sender, args) => { bodyIndexFrame = args.FrameData; uploadBodyIndex = true; };


            //Get coordinate map + rgb
            ColorRGBAFrameData colorFrame = new ColorRGBAFrameData();
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { colorFrame = args.FrameData; uploadRgb = true; };

            CounterPointCloudBuffer pointCloudBuffer = new CounterPointCloudBuffer(device);
            ColorPointCloudBuffer colorBuffer = new ColorPointCloudBuffer(device);

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (uploadCamera)
                {
                    cameraTexture.Copy(context.Context, rgbFrame);
                    depthToColorTexture.Copy(context.Context, depthToColorFrame);
                    uploadCamera = false;
                }

                if (uploadBodyIndex)
                {
                    bodyIndexTexture.Copy(context.Context, bodyIndexFrame);
                    uploadBodyIndex = false;
                }

                if (uploadRgb)
                {
                    colorTexture.Copy(context.Context, colorFrame);
                    uploadRgb = false;
                }

                //Prepare compute shader
                context.Context.ComputeShader.Set(computeShader);
                context.Context.ComputeShader.SetShaderResource(0, cameraTexture.ShaderView);
                context.Context.ComputeShader.SetShaderResource(1, bodyIndexTexture.RawView); //Set raw view here, we do not sample
                context.Context.ComputeShader.SetShaderResource(2, colorTexture.ShaderView);
                context.Context.ComputeShader.SetShaderResource(3, depthToColorTexture.ShaderView);

                context.Context.ComputeShader.SetSampler(0, device.SamplerStates.LinearClamp);

                context.Context.ComputeShader.SetUnorderedAccessView(0, pointCloudBuffer.UnorderedView, 0); //Don't forget to set count to 0
                context.Context.ComputeShader.SetUnorderedAccessView(1, colorBuffer.UnorderedView);

                context.Context.Dispatch(Consts.DepthWidth / 8, Consts.DepthHeight / 8, 1); //No iDivUp here, since it's not needed
                context.Context.ComputeShader.SetUnorderedAccessView(0, null); //Make runtime happy, and if we don't unbind we can't set as srv
                context.Context.ComputeShader.SetUnorderedAccessView(1, null);
                context.Context.CopyStructureCount(indirectDrawBuffer.ArgumentBuffer, 0, pointCloudBuffer.UnorderedView);

                //Draw filter buffer
                context.RenderTargetStack.Push(swapChain);
                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);

                context.Context.VertexShader.Set(vertexShader);
                context.Context.PixelShader.Set(pixelShader);

                context.Context.VertexShader.SetShaderResource(0, pointCloudBuffer.ShaderView);
                context.Context.VertexShader.SetShaderResource(1, colorBuffer.ShaderView);
                context.Context.VertexShader.SetConstantBuffer(0, cameraBuffer.Buffer);

                nullGeom.Bind(context, null);
                context.Context.DrawInstancedIndirect(indirectDrawBuffer.ArgumentBuffer, 0);

                //Make runtime happy
                context.Context.VertexShader.SetShaderResource(0, null);
                context.Context.VertexShader.SetShaderResource(1, null);

                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            cameraBuffer.Dispose();
            cameraTexture.Dispose();
            bodyIndexTexture.Dispose();

            provider.Dispose();
            bodyIndexProvider.Dispose();

            pointCloudBuffer.Dispose();
            colorBuffer.Dispose();

            colorTexture.Dispose();
            colorProvider.Dispose();

            depthToColorFrame.Dispose();
            depthToColorTexture.Dispose();

            computeShader.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
            sensor.Close();
        }
    }
}
