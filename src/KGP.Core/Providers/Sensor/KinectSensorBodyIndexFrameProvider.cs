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
    /// Kinect sensor version for body index frame provider
    /// </summary>
    public class KinectSensorBodyIndexFrameProvider : IBodyIndexFrameProvider, IDisposable
    {
        private readonly KinectSensor sensor;
        private BodyIndexFrameReader reader;
        private BodyIndexFrameData frameData;

        /// <summary>
        /// Raised when a new body index frame is received
        /// </summary>
        public event EventHandler<BodyIndexFrameDataEventArgs> FrameReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public KinectSensorBodyIndexFrameProvider(KinectSensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");

            this.sensor = sensor;
            this.reader = this.sensor.BodyIndexFrameSource.OpenReader();
            this.reader.FrameArrived += this.FrameArrived;
            this.frameData = new BodyIndexFrameData();
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

        private void FrameArrived(object sender, BodyIndexFrameArrivedEventArgs e)
        {
            var frame = e.FrameReference.AcquireFrame();
            if (frame != null)
            {
                frame.CopyFrameDataToIntPtr(this.frameData.DataPointer, (uint)this.frameData.SizeInBytes);
                if (this.FrameReceived != null)
                {
                    this.FrameReceived(this, new BodyIndexFrameDataEventArgs(this.frameData));
                }
            }
        }
    }
}
