using FeralTic.DX11;
using FeralTic.DX11.Resources;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Network.FrameServer;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalStreamerSample
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

            RenderForm form = new RenderForm("Kinect depth local stream sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            KinectFrameServer frameServer = new KinectFrameServer(32000, sensor);

            KinectFrameClient frameClient = new KinectFrameClient(IPAddress.Parse("127.0.0.1"), 32000);

            frameClient.Connect();


            bool doQuit = false;


            bool uploadDepth = false;
            bool uploadBody = false;

            bool showBody = false;
            
            DepthFrameData depthData = null;
            DynamicDepthTexture depth = new DynamicDepthTexture(device);

            IDepthFrameProvider networkDepth = (IDepthFrameProvider)frameClient;
            networkDepth.FrameReceived += (sender, args) => { depthData = args.DepthData; uploadDepth = true; };

            BodyIndexFrameData bodyIndexData = null;
            DynamicBodyIndexTexture bodyIndexTexture = new DynamicBodyIndexTexture(device);

            IBodyIndexFrameProvider networkBody = (IBodyIndexFrameProvider)frameClient;
            networkBody.FrameReceived += (sender, args) => { bodyIndexData = args.FrameData; uploadBody = true; };


            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } if (args.KeyCode == Keys.Space) { showBody = !showBody; } };

            RenderLoop.Run(form, () =>
            {
                if (doQuit)
                {
                    form.Dispose();
                    return;
                }

                if (uploadDepth)
                {
                    depth.Copy(context, depthData);
                    uploadDepth = false;
                }

                if (uploadBody)
                {
                    bodyIndexTexture.Copy(context, bodyIndexData);
                    uploadBody = false;
                }

                context.RenderTargetStack.Push(swapChain);

                if (showBody)
                {
                    device.Primitives.ApplyFullTri(context, bodyIndexTexture.NormalizedView);

                }
                else
                {
                    device.Primitives.ApplyFullTri(context, depth.NormalizedView);

                }
               
                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();
                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });
            
            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            depth.Dispose();
            bodyIndexTexture.Dispose();
            frameClient.Stop();
            frameServer.Dispose();

            sensor.Close();
        }
    }
}
