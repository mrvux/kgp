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
    /// Kinect sensor version for depth frame provider
    /// </summary>
    public class KinectSensorDepthFrameProvider : IDepthFrameProvider, IDisposable
    {
        private readonly KinectSensor sensor;
        private DepthFrameReader depthReader;
        private DepthFrameData frameData;

        /// <summary>
        /// Raised when a new depth frame is received
        /// </summary>
        public event EventHandler<DepthFrameDataEventArgs> FrameReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public KinectSensorDepthFrameProvider(KinectSensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");

            this.sensor = sensor;
            this.depthReader = this.sensor.DepthFrameSource.OpenReader();
            this.depthReader.FrameArrived += depthReader_FrameArrived;
            this.frameData = new DepthFrameData();
        }

        /// <summary>
        /// Dispose frame data
        /// </summary>
        public void Dispose()
        {
            this.depthReader.FrameArrived -= this.depthReader_FrameArrived;
            this.depthReader.Dispose();
            this.frameData.Dispose();
        }

        void depthReader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            var frame = e.FrameReference.AcquireFrame();
            if (frame != null)
            {
                frame.CopyFrameDataToIntPtr(this.frameData.DataPointer, (uint)this.frameData.SizeInBytes);
                frame.Dispose();
                if (this.FrameReceived != null)
                {
                    this.FrameReceived(this, new DepthFrameDataEventArgs(this.frameData));
                }
            }
        }
    }
}
