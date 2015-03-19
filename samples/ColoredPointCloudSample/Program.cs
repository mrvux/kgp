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

namespace ColoredPointCloudSample
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

            RenderForm form = new RenderForm("Kinect Simple point cloud view sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ColoredPointCloudView.fx", "VS");
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ColoredPointCloudView.fx", "PS");

            DX11NullInstancedDrawer nulldrawer = new DX11NullInstancedDrawer();
            nulldrawer.VertexCount = Consts.DepthWidth;
            nulldrawer.InstanceCount = Consts.DepthHeight;
            DX11NullGeometry nullGeom = new DX11NullGeometry(device, nulldrawer);
            nullGeom.Topology = SharpDX.Direct3D.PrimitiveTopology.PointList;


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
            bool uploadRgb = false;

            DepthToColorFrameData depthToColorFrame = new DepthToColorFrameData();
            CameraRGBFrameData cameraFrame = new CameraRGBFrameData();
            DynamicCameraRGBTexture cameraTexture = new DynamicCameraRGBTexture(device);
            DynamicDepthToColorTexture depthToColorTexture = new DynamicDepthToColorTexture(device);

            KinectSensorDepthFrameProvider provider = new KinectSensorDepthFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { cameraFrame.Update(sensor.CoordinateMapper, args.DepthData); depthToColorFrame.Update(sensor.CoordinateMapper, args.DepthData); uploadCamera = true; };

            //Get coordinate map + rgb
            ColorRGBAFrameData colorFrame = new ColorRGBAFrameData();
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { colorFrame = args.FrameData; uploadRgb = true; };

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
                    cameraTexture.Copy(context.Context, cameraFrame);
                    depthToColorTexture.Copy(context.Context, depthToColorFrame);
                    uploadCamera = false;
                }

                if (uploadRgb)
                {
                    colorTexture.Copy(context.Context, colorFrame);
                    uploadRgb = false;
                }

                context.RenderTargetStack.Push(swapChain);
                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);

                context.Context.VertexShader.Set(vertexShader);
                context.Context.PixelShader.Set(pixelShader);

                context.Context.VertexShader.SetShaderResource(0, cameraTexture.ShaderView);
                context.Context.VertexShader.SetShaderResource(1, colorTexture.ShaderView);
                context.Context.VertexShader.SetShaderResource(2, depthToColorTexture.ShaderView);

                context.Context.VertexShader.SetSampler(0, device.SamplerStates.LinearClamp);

                context.Context.VertexShader.SetConstantBuffer(0, cameraBuffer.Buffer);

                nullGeom.Bind(context, null);
                nullGeom.Draw(context);

                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            cameraBuffer.Dispose();
            cameraTexture.Dispose();

            provider.Dispose();

            pixelShader.Dispose();
            vertexShader.Dispose();
            sensor.Close();

            colorTexture.Dispose();
            colorProvider.Dispose();

            depthToColorFrame.Dispose();
            depthToColorTexture.Dispose();
        }
    }
}
