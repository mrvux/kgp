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
    /// Kinect sensor version for infrared frame provider
    /// </summary>
    public class KinectSensorInfraredFrameProvider : IInfraredFrameProvider, IDisposable
    {
        private readonly KinectSensor sensor;
        private InfraredFrameReader reader;
        private InfraredFrameData frameData;

        /// <summary>
        /// Raised when a new infrared frame is received
        /// </summary>
        public event EventHandler<InfraredFrameDataEventArgs> FrameReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public KinectSensorInfraredFrameProvider(KinectSensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");

            this.sensor = sensor;
            this.reader = this.sensor.InfraredFrameSource.OpenReader();
            this.reader.FrameArrived += FrameArrived;
            this.frameData = new InfraredFrameData();
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

        private void FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            var frame = e.FrameReference.AcquireFrame();
            if (frame != null)
            {
                frame.CopyFrameDataToIntPtr(this.frameData.DataPointer, (uint)this.frameData.SizeInBytes);
                frame.Dispose();
                if (this.FrameReceived != null)
                {
                    this.FrameReceived(this, new InfraredFrameDataEventArgs(this.frameData));
                }
            }
        }
    }
}
