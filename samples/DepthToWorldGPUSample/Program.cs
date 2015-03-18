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

namespace DepthToWorldGPUSample
{
    static class Program
    {
        static string header = "Kinect depth to world GPU sample";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RenderForm form = new RenderForm(header);

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            PixelShader pixelShaderRaw = ShaderCompiler.CompileFromFile<PixelShader>(device, "DepthToWorld.fx", "PS_Raw");
            PixelShader pixelShaderNorm = ShaderCompiler.CompileFromFile<PixelShader>(device, "DepthToWorld.fx", "PS_Normalized");

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            RayTableTexture rayTable = RayTableTexture.FromCoordinateMapper(device, sensor.CoordinateMapper);
            RenderCameraTexture renderCamera = new RenderCameraTexture(device);

            bool doQuit = false;
            bool doUpload = false;
            bool useRaw = true;

            DepthFrameData currentData = null;
            DynamicDepthTexture depth = new DynamicDepthTexture(device);
            KinectSensorDepthFrameProvider provider = new KinectSensorDepthFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { currentData = args.DepthData; doUpload = true; };

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } if (args.KeyCode == Keys.Space) { useRaw = !useRaw; } };

            RenderLoop.Run(form, () =>
            {
                form.Text = header + (useRaw ? " - Raw Mode": " - Normalized Mode");

                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (doUpload)
                {
                    depth.Copy(context, currentData);

                    //Convert depth to world
                    context.Context.OutputMerger.SetRenderTargets(renderCamera.RenderView);
                    device.Primitives.ApplyFullTriVS(context);

                    if (useRaw)
                    {
                        context.Context.PixelShader.Set(pixelShaderRaw);
                        context.Context.PixelShader.SetShaderResource(0, depth.RawView);
                    }
                    else
                    {
                        context.Context.PixelShader.Set(pixelShaderNorm);
                        context.Context.PixelShader.SetShaderResource(0, depth.NormalizedView);
                    }
                    
                    context.Context.PixelShader.SetShaderResource(1, rayTable.ShaderView);

                    device.Primitives.FullScreenTriangle.Draw(context);
                    context.RenderTargetStack.Apply();
                }

                context.RenderTargetStack.Push(swapChain);

                device.Primitives.ApplyFullTri(context, renderCamera.ShaderView);

                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            depth.Dispose();
            renderCamera.Dispose();
            rayTable.Dispose();
            provider.Dispose();

            pixelShaderNorm.Dispose();
            pixelShaderRaw.Dispose();

            sensor.Close();
        }
    }
}
