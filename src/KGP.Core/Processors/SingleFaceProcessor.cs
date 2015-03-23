using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Tracks a single face
    /// </summary>
    public class SingleFaceProcessor : IDisposable
    {
        private FaceFrameSource frameSource;
        private FaceFrameReader framereader;

        /// <summary>
        /// Raised when we received a valid face frame
        /// </summary>
        public event EventHandler<FaceFrameResultEventArgs> FaceResultAcquired;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public SingleFaceProcessor(KinectSensor sensor)
        {
            this.frameSource = new FaceFrameSource(sensor, 0, FaceUtils.AllFeatures());
            this.framereader = this.frameSource.OpenReader();
            this.framereader.FrameArrived += this.FrameArrived;
        }

        private void FrameArrived(object sender, FaceFrameArrivedEventArgs e)
        {
            using (FaceFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (frame.IsTrackingIdValid == false) { return; }
                    if (this.FaceResultAcquired != null)
                    {
                        this.FaceResultAcquired(this, new FaceFrameResultEventArgs(this.frameSource.TrackingId, frame.FaceFrameResult));
                    }
                }
            }
        }

        /// <summary>
        /// Assigns a kinect body for face tracking
        /// </summary>
        /// <param name="body">Body to assign</param>
        public void AssignBody(KinectBody body)
        {
            this.frameSource.TrackingId = body.TrackingId;
            this.framereader.IsPaused = false;
        }

        /// <summary>
        /// Pauses tracking
        /// </summary>
        public void Suspend()
        {
            this.framereader.IsPaused = true;
        }

        /// <summary>
        /// Check if tracking id is valid
        /// </summary>
        public bool IsValid
        {
            get { return this.frameSource.IsTrackingIdValid; }
        }

        /// <summary>
        /// Dispose class
        /// </summary>
        public void Dispose()
        {
            this.framereader.FrameArrived -= this.FrameArrived;
            this.framereader.Dispose();
        }
    }
}
