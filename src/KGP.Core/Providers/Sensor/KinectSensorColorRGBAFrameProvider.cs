using KGP.Frames;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers.Sensor
{
    /// <summary>
    /// Kinect sensor version for color frame provider (as rgba)
    /// </summary>
    public class KinectSensorColorRGBAFrameProvider : IColorRGBAFrameProvider, IDisposable
    {
        private readonly KinectSensor sensor;
        private ColorFrameReader reader;
        private ColorRGBAFrameData frameData;

        /// <summary>
        /// Raised when a new color frame is received
        /// </summary>
        public event EventHandler<ColorRGBAFrameDataEventArgs> FrameReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public KinectSensorColorRGBAFrameProvider(KinectSensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");

            this.sensor = sensor;
            this.reader = this.sensor.ColorFrameSource.OpenReader();
            this.reader.FrameArrived += FrameArrived;
            this.frameData = new ColorRGBAFrameData();
        }

        /// <summary>
        /// Dispose frame data
        /// </summary>
        public void Dispose()
        {
            this.reader.FrameArrived -= this.FrameArrived;
            this.reader.Dispose();
            this.frameData.Dispose();
        }

        void FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            var frame = e.FrameReference.AcquireFrame();
            if (frame != null)
            {
                frame.CopyConvertedFrameDataToIntPtr(this.frameData.DataPointer, (uint)this.frameData.SizeInBytes, ColorImageFormat.Rgba);
                frame.Dispose();
                if (this.FrameReceived != null)
                {
                    this.FrameReceived(this, new ColorRGBAFrameDataEventArgs(this.frameData));
                }
            }
        }
    }
}
