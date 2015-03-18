using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Processes body list and checks for hand state changes
    /// </summary>
    public class HandStateTrackingProcessor
    {
        private IEnumerable<KinectBody> lastBodyFrame;

        /// <summary>
        /// Constructs a tracking processor instance (with an empty tracking record)
        /// </summary>
        public HandStateTrackingProcessor()
        {
            this.lastBodyFrame = new KinectBody[0];
        }

        /// <summary>
        /// Raised when a new body is tracked
        /// </summary>
        public event EventHandler<KinectBodyEventArgs> BodyTrackingStarted;

        /// <summary>
        /// Raised when body tracking is lost
        /// </summary>
        public event EventHandler<KinectBodyEventArgs> BodyTrackingLost;

        /// <summary>
        /// Process next frame
        /// </summary>
        /// <param name="bodies">Body list provided from kinect</param>
        /// <remarks>Here we filter with tracking state, there is no need to prefilter</remarks>
        public void Next(IEnumerable<KinectBody> bodies)
        {
            IEnumerable<KinectBody> trackedBodies = bodies.TrackedOnly();

            //Search for lost bodies first
            foreach (KinectBody kb in lastBodyFrame)
            {
                if (!kb.ContainsId(trackedBodies))
                {
                    this.RaiseBodyTrackingLost(kb);
                }
            }

            foreach (KinectBody kb in trackedBodies)
            {
                if (!kb.ContainsId(lastBodyFrame))
                {
                    this.RaiseBodyTrackingStarted(kb);
                }
            }
  
            this.lastBodyFrame = trackedBodies;
        }

        private void RaiseBodyTrackingStarted(KinectBody body)
        { 
            if (this.BodyTrackingStarted != null)
            {
                this.BodyTrackingStarted(this,new KinectBodyEventArgs(body));
            }
        }

        private void RaiseBodyTrackingLost(KinectBody body)
        {
            if (this.BodyTrackingLost != null)
            {
                this.BodyTrackingLost(this, new KinectBodyEventArgs(body));
            }
        }
    }
}
