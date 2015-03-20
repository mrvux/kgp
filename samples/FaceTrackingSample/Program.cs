using FeralTic.DX11;
using FeralTic.DX11.Resources;
using KGP;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Processors;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using SharpDX;
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

            RenderForm form = new RenderForm("Kinect color sample");

            RenderDevice device = new RenderDevice(SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
            RenderContext context = new RenderContext(device);
            DX11SwapChain swapChain = DX11SwapChain.FromHandle(device, form.Handle);

            //Allow to draw using direct2d on top of swapchain
            var context2d = new SharpDX.Direct2D1.DeviceContext(swapChain.Texture.QueryInterface<SharpDX.DXGI.Surface>());
            //Call release on texture since queryinterface does an addref
            Marshal.Release(swapChain.Texture.NativePointer);

            var whiteBrush = new SharpDX.Direct2D1.SolidColorBrush(context2d, SharpDX.Color.White);

            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            KinectBody[] bodyFrame = null;
            KinectSensorBodyFrameProvider bodyProvider = new KinectSensorBodyFrameProvider(sensor);

            bool doQuit = false;
            bool doUpload = false;
            ColorRGBAFrameData currentData = null;
            DynamicColorRGBATexture colorTexture = new DynamicColorRGBATexture(device);
            KinectSensorColorRGBAFrameProvider provider = new KinectSensorColorRGBAFrameProvider(sensor);
            provider.FrameReceived += (sender, args) => { currentData = args.FrameData; doUpload = true; };

            form.KeyDown += (sender, args) => { if (args.KeyCode == Keys.Escape) { doQuit = true; } };


            FaceFrameResult frameResult = null;
            SingleFaceProcessor faceProcessor = new SingleFaceProcessor(sensor);
            faceProcessor.FaceResultAcquired += (sender, args) => { frameResult = args; };

            Func<PointF, Vector2> map = new Func<PointF, Vector2>((p) =>
            {
                float x = p.X / 1920.0f * (float)swapChain.Width;
                float y = p.Y / 1080.0f * (float)swapChain.Height;
                return new Vector2(x,y);
            });

            Func<float,float, Vector2> mapxy = new Func<float,float, Vector2>((px,py) =>
            {
                float x = px / 1920.0f * (float)swapChain.Width;
                float y = py / 1080.0f * (float)swapChain.Height;
                return new Vector2(x,y);
            });

            bodyProvider.FrameReceived += (sender, args) =>
            {
                bodyFrame = args.FrameData;
                var body = bodyFrame.TrackedOnly().ClosestBodies().FirstOrDefault();
                if (body != null)
                {
                    faceProcessor.AssignBody(body);
                }
                else
                {
                    faceProcessor.Suspend();
                }
            };

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
                }

                context.RenderTargetStack.Push(swapChain);

                device.Primitives.ApplyFullTri(context, colorTexture.ShaderView);

                device.Primitives.FullScreenTriangle.Draw(context);
                context.RenderTargetStack.Pop();

                if (frameResult != null)
                {
                    context2d.BeginDraw();
                    var colorBound = frameResult.FaceBoundingBoxInColorSpace;
                    RectangleF rect = new RectangleF();
                    Vector2 topLeft = mapxy(colorBound.Left, colorBound.Top);
                    Vector2 bottomRight = mapxy(colorBound.Right, colorBound.Bottom);
                    rect.Top = topLeft.Y;
                    rect.Bottom = bottomRight.Y;
                    rect.Left = topLeft.X;
                    rect.Right = bottomRight.X;

                    context2d.DrawRectangle(rect, whiteBrush, 3.0f);

                    foreach (PointF point in frameResult.FacePointsInColorSpace.Values)
                    {
                        var ellipse = new SharpDX.Direct2D1.Ellipse()
                        {
                            Point = map(point),
                            RadiusX = 5,
                            RadiusY = 5
                        };

                        context2d.FillEllipse(ellipse, whiteBrush);
                    }

                    context2d.EndDraw();
                }

                swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            });

            swapChain.Dispose();
            context.Dispose();
            device.Dispose();

            colorTexture.Dispose();
            provider.Dispose();

            bodyProvider.Dispose();
            faceProcessor.Dispose();

            whiteBrush.Dispose();
            context2d.Dispose();

            sensor.Close();
        }
    }
}
