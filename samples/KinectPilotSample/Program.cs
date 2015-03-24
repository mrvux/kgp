using FeralTic.DX11;
using FeralTic.DX11.Resources;
using KGP;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Processors;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorTextureSample
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

            RenderForm form = new RenderForm("Kinect simple pilot sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            //Allow to draw using direct2d on top of swapchain
            var context2d = new SharpDX.Direct2D1.DeviceContext(swapChain.Texture.QueryInterface<SharpDX.DXGI.Surface>());
            //Call release on texture since queryinterface does an addref
            Marshal.Release(swapChain.Texture.NativePointer);

            var textFormat = new SharpDX.DirectWrite.TextFormat(device.DWriteFactory, "Arial", 16.0f);


            var blackBrush = new SharpDX.Direct2D1.SolidColorBrush(context2d, SharpDX.Color.Black);
            var whiteBrush = new SharpDX.Direct2D1.SolidColorBrush(context2d, SharpDX.Color.White);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            bool doQuit = false;
            bool doUpload = false;
            ColorRGBAFrameData currentData = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider provider = new KinectSensorColorRGBAFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { currentData = args.FrameData; doUpload = true; };

            KinectPilotProcessor pilot = KinectPilotProcessor.Default;

            KinectSensorBodyFrameProvider bodyFrameProvider = new KinectSensorBodyFrameProvider(sensor);
            bodyFrameProvider.FrameReceived += (sender, args) => 
            {
                var body = args.FrameData.TrackedOnly().ClosestBodies().FirstOrDefault();
                if (body != null)
                {
                    pilot.Process(body.GetJointTable());
                }
            };

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
                    colorTexture.Copy(context, currentData);
                    doUpload = false;
                }

                context.RenderTargetStack.Push(swapChain);

                device.Primitives.ApplyFullTri(context, colorTexture.ShaderView);
                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();

                context2d.BeginDraw();

                var rect = new SharpDX.RectangleF(0, 0, 200, 130);
                context2d.FillRectangle(rect, blackBrush);
                context2d.DrawText("Elevation: " + pilot.Elevation, textFormat, rect, whiteBrush);
                rect.Top += 30;
                context2d.DrawText("Steering Y: " + pilot.SteeringY, textFormat, rect, whiteBrush);
                rect.Top += 30;
                context2d.DrawText("Steering Z: " + pilot.SterringZ, textFormat, rect, whiteBrush);
                rect.Top += 30;
                context2d.DrawText("Push: " + pilot.Push, textFormat, rect, whiteBrush);
                context2d.EndDraw();


                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            colorTexture.Dispose();
            provider.Dispose();

            sensor.Close();
        }
    }
}
