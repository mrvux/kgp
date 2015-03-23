using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Event args wrapper fro Frace frame result
    /// </summary>
    public class FaceFrameResultEventArgs
    {
        private readonly ulong trackingId;
        private readonly FaceFrameResult frameResult;

        /// <summary>
        /// Tracking Id
        /// </summary>
        public ulong TrackingId
        {
            get { return this.trackingId; }
        }

        /// <summary>
        /// Frame result
        /// </summary>
        public FaceFrameResult FrameResult
        {
            get { return this.frameResult; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trackingId">Tracking ID</param>
        /// <param name="frameResult">Frame result</param>
        public FaceFrameResultEventArgs(ulong trackingId, FaceFrameResult frameResult)
        {
            if (frameResult == null)
                throw new ArgumentNullException("frameResult");

            this.trackingId = trackingId;
            this.frameResult = frameResult;
        }
    }
}
