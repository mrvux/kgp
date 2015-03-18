using Microsoft.Kinect;
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
        public event EventHandler<KinectHandStateEventArgs> HandStateChanged;

        /// <summary>
        /// Process next frame
        /// </summary>
        /// <param name="bodies">Body list provided from kinect</param>
        /// <remarks>Here we filter with tracking state, there is no need to prefilter</remarks>
        public void Next(IEnumerable<KinectBody> bodies)
        {
            IEnumerable<KinectBody> trackedBodies = bodies.TrackedOnly();

            foreach (KinectBody kb in trackedBodies)
            {
                if (kb.ContainsId(lastBodyFrame))
                {
                    KinectBody previous = kb.FindById(lastBodyFrame);

                    if (previous.HandLeftState != kb.HandLeftState)
                    {
                        this.RaiseHandStateChanged(kb, HandType.Left, previous.HandLeftState);
                    }

                    if (previous.HandRightState != kb.HandRightState)
                    {
                        this.RaiseHandStateChanged(kb, HandType.Right, previous.HandRightState);
                    }
                }
            }
  
            this.lastBodyFrame = trackedBodies;
        }

        private void RaiseHandStateChanged(KinectBody body, HandType handType, HandState previousState)
        {
            if (this.HandStateChanged != null)
            {
                this.HandStateChanged(this, new KinectHandStateEventArgs(body, handType, previousState));
            }
        }
    }
}
