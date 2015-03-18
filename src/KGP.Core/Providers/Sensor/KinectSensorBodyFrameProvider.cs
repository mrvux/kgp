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
    /// Kinect sensor version for body frame provider
    /// </summary>
    public class KinectSensorBodyFrameProvider : IBodyFrameProvider, IDisposable
    {
        private readonly KinectSensor sensor;
        private BodyFrameReader reader;
        private Body[] bodies = new Body[Consts.MaxBodyCount];

        /// <summary>
        /// Raised when a new body index frame is received
        /// </summary>
        public event EventHandler<KinectBodyFrameDataEventArgs> FrameReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public KinectSensorBodyFrameProvider(KinectSensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");

            this.sensor = sensor;
            this.reader = this.sensor.BodyFrameSource.OpenReader();
            this.reader.FrameArrived += this.FrameArrived;
        }

        /// <summary>
        /// Dispose frame data
        /// </summary>
        public void Dispose()
        {
            this.reader.FrameArrived -= this.FrameArrived;
            this.reader.Dispose();
        }

        private void FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            var frame = e.FrameReference.AcquireFrame();
            if (frame != null)
            {
                frame.GetAndRefreshBodyData(this.bodies);
                frame.Dispose();

                KinectBody[] newFrame = this.bodies.Select(body => new KinectBody(body)).ToArray();

                if (this.FrameReceived != null)
                {
                    this.FrameReceived(this, new KinectBodyFrameDataEventArgs(newFrame));
                }
            }
        }
    }
}
