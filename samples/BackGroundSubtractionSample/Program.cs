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

namespace BackgroundSubtractionSample
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

            RenderForm form = new RenderForm("Kinect background subtraction sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            PixelShader depthPixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "FilterDepthView.fx", "PS_Sample");
            PixelShader rgbPixelShader = ShaderCompiler.CompileFromFile<PixelShader>(device, "FilterRGBView.fx", "PS_Sample");

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();



            bool doQuit = false;
            bool swapMode = false;

            bool uploadColor = false;
            bool uploadBodyIndex = false;

            //We need color and body index for subtraction
            ColorRGBAFrameData colorData = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider colorProvider = new KinectSensorColorRGBAFrameProvider(sensor);
            colorProvider.FrameReceived += (sender, args) => { colorData = args.FrameData; uploadColor = true; };

            BodyIndexFrameData bodyIndexData = null;
            DynamicBodyIndexTexture bodyIndexTexture = new DynamicBodyIndexTexture(device);
            KinectSensorBodyIndexFrameProvider bodyIndexProvider = new KinectSensorBodyIndexFrameProvider(sensor);
            bodyIndexProvider.FrameReceived += (sender, args) => { bodyIndexData = args.FrameData; uploadBodyIndex = true; };

           
            bool uploadColorToDepth = false;
            bool uploadDepthToColor = false;
            ColorToDepthFrameData colorToDepthData = new ColorToDepthFrameData();
            DepthToColorFrameData depthToColorData = new DepthToColorFrameData();
            KinectSensorDepthFrameProvider depthProvider = new KinectSensorDepthFrameProvider(sensor);
            depthProvider.FrameReceived += (sender, args) =>
            { 
                if (!swapMode)
                {
                    colorToDepthData.Update(sensor.CoordinateMapper, args.DepthData);
                    uploadColorToDepth = true; 
                }
                else
                {
                    depthToColorData.Update(sensor.CoordinateMapper, args.DepthData);
                    uploadDepthToColor = true;
                }         
            };
            
            DynamicColorToDepthTexture colorToDepthTexture = new DynamicColorToDepthTexture(device);
            DynamicDepthToColorTexture depthToColorTexture = new DynamicDepthToColorTexture(device);


            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } if (args.KeyCode == Keys.Space) { swapMode = !swapMode; } };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (uploadColor)
                {
                    colorTexture.Copy(context, colorData);
                    uploadColor = false;
                }

                if (uploadBodyIndex)
                {
                    bodyIndexTexture.Copy(context, bodyIndexData);
                    uploadBodyIndex = false;
                }

                if (uploadColorToDepth)
                {
                    colorToDepthTexture.Copy(context, colorToDepthData);
                    uploadColorToDepth = false;
                }

                if (uploadDepthToColor)
                {
                    depthToColorTexture.Copy(context, depthToColorData);
                    uploadDepthToColor = false;
                }



                ShaderResourceView view = swapMode ? depthToColorTexture.ShaderView : colorToDepthTexture.ShaderView;
                PixelShader shader = swapMode ? depthPixelShader : rgbPixelShader;

                context.RenderTargetStack.Push(swapChain);

                device.Primitives.ApplyFullTriVS(context);

                context.Context.PixelShader.Set(shader);
                context.Context.PixelShader.SetShaderResource(0, colorTexture.ShaderView);
                //Note: make sure to use normalized view as we use untyped resource here
                context.Context.PixelShader.SetShaderResource(1, bodyIndexTexture.NormalizedView);
                context.Context.PixelShader.SetShaderResource(2, view);
                
                context.Context.PixelShader.SetSampler(0, device.SamplerStates.LinearClamp);


                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            depthProvider.Dispose();
            colorToDepthData.Dispose();
            depthToColorData.Dispose();
            colorToDepthTexture.Dispose();
            depthToColorTexture.Dispose();

            colorTexture.Dispose();
            colorProvider.Dispose();

            bodyIndexData.Dispose();
            bodyIndexProvider.Dispose();

            depthPixelShader.Dispose();
            rgbPixelShader.Dispose();

            sensor.Close();
        }
    }
}
