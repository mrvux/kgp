using FeralTic.DX11;
using FeralTic.DX11.Resources;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepthTextureSample
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

            RenderForm form = new RenderForm("Kinect body index sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            PixelShader pixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "BodyIndexView.fx", "PS_NormalizedView");
 
            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();



            bool doQuit = false;
            bool doUpload = false;
            BodyIndexFrameData currentData = null;
            DynamicBodyIndexTexture texture = new DynamicBodyIndexTexture(device);
            KinectSensorBodyIndexFrameProvider provider = new KinectSensorBodyIndexFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { currentData = args.FrameData; doUpload = true; };

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
                    texture.Copy(context, currentData);
                    doUpload = false;
                }

                context.RenderTargetStack.Push(swapChain);

                device.Primitives.ApplyFullTriVS(context);
                
                context.Context.PixelShader.Set(pixelShader);
                context.Context.PixelShader.SetSampler(0, device.SamplerStates.LinearClamp);
                context.Context.PixelShader.SetShaderResource(0, texture.NormalizedView);

                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            texture.Dispose();
            provider.Dispose();

            pixelShader.Dispose();

            sensor.Close();
        }
    }
}
