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

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            //VertexShader vertexShader = ShaderCompiler.CompileFromFile<VertexShader>(device, "ColorJointView.fx", "VS");
            SharpDX.D3DCompiler.ShaderSignature signature;
            VertexShader vertexShader = ShaderCompiler.CompileFromFile(device, "ColorJointView.fx", "VS_Color", out signature);
            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "ColorJointView.fx", "PS_Color");

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

            Color4[] statusColor = new Color4[]
            {
                Color.Red,
                Color.Yellow,
                Color.Green
            };

            //Note cbuffer should have a minimum size of 16 bytes, so we create verctor4 instead of vector2
            SharpDX.Vector4 jointSize = new SharpDX.Vector4(0.04f,0.07f,0.0f,1.0f);
            ConstantBuffer<SharpDX.Vector4> cbSize = new ConstantBuffer<SharpDX.Vector4>(device);
            cbSize.Update(context, ref jointSize);

            DX11StructuredBuffer colorTableBuffer = DX11StructuredBuffer.CreateImmutable<Color4>(device, statusColor);

            bool doQuit = false;
            bool doUpload = false;
            bool uploadImage = false;
            

            KinectBody[] bodyFrame = null;
            BodyColorPositionBuffer positionBuffer = new BodyColorPositionBuffer(device);
            BodyJointStatusBuffer statusBuffer = new BodyJointStatusBuffer(device);

            KinectSensorBodyFrameProvider provider = new KinectSensorBodyFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { bodyFrame = args.FrameData; doUpload = true; };

            ColorRGBAFrameData rgbFrame = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { rgbFrame = args.FrameData; uploadImage = true; };


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
                    var tracked = bodyFrame.TrackedOnly();
                    var colorSpace = tracked.Select(kb => new ColorSpaceKinectJoints(kb, sensor.CoordinateMapper));
                    
                    positionBuffer.Copy(context, colorSpace);
                    statusBuffer.Copy(context, tracked);
                    drawer.InstanceCount = colorSpace.Count() * Microsoft.Kinect.Body.JointCount;
                }

                if (uploadImage)
                {
                    colorTexture.Copy(context, rgbFrame);
                }

                context.RenderTargetStack.Push(swapChain);
                context.Context.ClearRenderTargetView(swapChain.RenderView, SharpDX.Color.Black);

                device.Primitives.ApplyFullTri(context, colorTexture.ShaderView);
                device.Primitives.FullScreenTriangle.Draw(context);

                circle.Bind(context, layout);
                
                context.Context.PixelShader.Set(pixelShader);
                context.Context.VertexShader.Set(vertexShader);
                context.Context.VertexShader.SetShaderResource(0, positionBuffer.ShaderView);
                context.Context.VertexShader.SetShaderResource(1, statusBuffer.ShaderView);
                context.Context.VertexShader.SetShaderResource(2, colorTableBuffer.ShaderView);
                context.Context.VertexShader.SetConstantBuffer(0, cbSize.Buffer);

                circle.Draw(context);

                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            colorProvider.Dispose();
            colorTexture.Dispose();

            positionBuffer.Dispose();
            statusBuffer.Dispose();
            colorTableBuffer.Dispose();

            cbSize.Dispose();

            provider.Dispose();
            circle.Dispose();
            layout.Dispose();



            pixelShader.Dispose();
            vertexShader.Dispose();


            sensor.Close();
        }
    }
}
