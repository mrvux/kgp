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
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JointColorSample
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

            RenderForm form = new RenderForm("Kinect RGB Joint sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            //VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ColorJointView.fx", "VS");
            SharpDX.D3DCompiler.ShaderSignature signature;
            VertexShader vertexShader = ShaderCompiler.CompileFromFile(device, "ColorJointView.fx", "VS", out signature);
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ColorJointView.fx", "PS");

            DX11IndexedGeometry circle = device.Primitives.Segment(new Segment()
            {
                Resolution = 32
            });
            DX11InstancedIndexedDrawer drawer = new DX11InstancedIndexedDrawer();
            circle.AssignDrawer(drawer);

            InputLayout layout;
            var bc = new ShaderBytecode(signature);
            circle.ValidateLayout(bc, out layout);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();



            bool doQuit = false;
            bool doUpload = false;

            KinectBody[] bodyFrame = null;
            BodyColorPositionBuffer positionBuffer = new BodyColorPositionBuffer(device);

            KinectSensorBodyFrameProvider provider = new KinectSensorBodyFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { bodyFrame = args.FrameData; doUpload = true; };

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (doUpload)
                {
                    var colorSpace = bodyFrame.TrackedOnly().Select(kb => new ColorSpaceKinectJoints(kb, sensor.CoordinateMapper));
                    positionBuffer.Copy(context, colorSpace);
                    drawer.InstanceCount = colorSpace.Count() * Microsoft.Kinect.Body.JointCount;
                }

                context.RenderTargetStack.Push(swapChain);
                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);


                context.Context.PixelShader.Set(pixelShader);
                context.Context.VertexShader.Set(vertexShader);
                context.Context.VertexShader.SetShaderResource(0, positionBuffer.ShaderView);

                circle.Bind(context, layout);
                circle.Draw(context);

                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            positionBuffer.Dispose();
            provider.Dispose();

            pixelShader.Dispose();
            vertexShader.Dispose();

            sensor.Close();
        }
    }
}
