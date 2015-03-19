using FeralTic.DX11;
using FeralTic.DX11.Geometry;
using FeralTic.DX11.Resources;
using KGP;
using KGP.Direct3D11.Buffers;
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

            RenderForm form = new RenderForm("Kinect Camera Joint sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            DX11DepthStencil depthStencil = new DX11DepthStencil(device, swapChain.Width, swapChain.Height, eDepthFormat.d24s8);


            //VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ColorJointView.fx", "VS");
            SharpDX.D3DCompiler.ShaderSignature signature;
            VertexShader vertexShader = ShaderCompiler.CompileFromFile(device, "CameraJointView.fx", "VS_Color", out signature);
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "CameraJointView.fx", "PS_Color");

            DX11IndexedGeometry cube = device.Primitives.Box(new Box()
            {
                Size = new Vector3(0.2f)
            });
            DX11InstancedIndexedDrawer drawer = new DX11InstancedIndexedDrawer();
            cube.AssignDrawer(drawer);

            InputLayout layout;
            var bc = new ShaderBytecode(signature);
            cube.ValidateLayout(bc, out layout);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            Color4[] statusColor = new Color4[]
            {
                Color.Red,
                Color.Yellow,
                Color.Green
            };

            cbCamera camera = new cbCamera();
            camera.Projection = Matrix.PerspectiveFovLH(1.57f, 1.3f, 0.1f, 100.0f);
            camera.View = Matrix.Translation(0.0f, 0.0f, 2.0f);

            camera.Projection.Transpose();
            camera.View.Transpose();

            ConstantBuffer<cbCamera> cameraBuffer = new ConstantBuffer<cbCamera>(device);
            cameraBuffer.Update(context, ref camera);

            DX11StructuredBuffer colorTableBuffer = DX11StructuredBuffer.CreateImmutable<Color4>(device, statusColor);

            bool doQuit = false;
            bool doUpload = false;
 

            KinectBody[] bodyFrame = null;
            BodyCameraPositionBuffer positionBuffer = new BodyCameraPositionBuffer(device);
            BodyJointStatusBuffer statusBuffer = new BodyJointStatusBuffer(device);

            KinectSensorBodyFrameProvider provider = new KinectSensorBodyFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { bodyFrame = args.FrameData; doUpload = true; };

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };


            context.Context.OutputMerger.DepthStencilState = device.DepthStencilStates.LessReadWrite;
            context.Context.Rasterizer.State = device.RasterizerStates.BackCullSolid;

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (doUpload)
                {
                    var tracked = bodyFrame.TrackedOnly();

                    positionBuffer.Copy(context, tracked);
                    statusBuffer.Copy(context, tracked);
                    drawer.InstanceCount = tracked.Count() * Microsoft.Kinect.Body.JointCount;
                }

                context.RenderTargetStack.Push(depthStencil, false,swapChain);
                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);
                depthStencil.Clear(context);

                cube.Bind(context, layout);

                context.Context.PixelShader.Set(pixelShader);
                context.Context.VertexShader.Set(vertexShader);
                context.Context.VertexShader.SetShaderResource(0, positionBuffer.ShaderView);
                context.Context.VertexShader.SetShaderResource(1, statusBuffer.ShaderView);
                context.Context.VertexShader.SetShaderResource(2, colorTableBuffer.ShaderView);
                context.Context.VertexShader.SetConstantBuffer(0, cameraBuffer.Buffer);

                cube.Draw(context);

                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            depthStencil.Dispose();
            context.Dispose();
            device.Dispose();

            positionBuffer.Dispose();
            statusBuffer.Dispose();
            colorTableBuffer.Dispose();

            cameraBuffer.Dispose();

            provider.Dispose();
            cube.Dispose();
            layout.Dispose();

            pixelShader.Dispose();
            vertexShader.Dispose();


            sensor.Close();
        }
    }
}
