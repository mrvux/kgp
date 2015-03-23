using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Frame result for hd face tracking
    /// </summary>
    public class HdFaceFrameResultEventArgs
    {
        private readonly ulong trackingId;
        private readonly FaceModel faceModel;
        private readonly FaceAlignment faceAlignment;

        /// <summary>
        /// Tracking Id
        /// </summary>
        public ulong TrackingId
        {
            get { return this.trackingId; }
        }

        /// <summary>
        /// Face model
        /// </summary>
        public FaceModel FaceModel
        {
            get { return this.faceModel; }
        }

        /// <summary>
        /// Face alignment
        /// </summary>
        public FaceAlignment FaceAlignment
        {
            get { return this.faceAlignment; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trackingId">Tracking Id</param>
        /// <param name="faceModel">Face Model</param>
        /// <param name="faceAlignment">Face Alignment</param>
        public HdFaceFrameResultEventArgs(ulong trackingId, FaceModel faceModel, FaceAlignment faceAlignment)
        {
            if (faceModel == null)
                throw new ArgumentNullException("faceModel");
            if (faceAlignment == null)
                throw new ArgumentNullException("faceAlignment");

            this.trackingId = trackingId;
            this.faceAlignment = faceAlignment;
            this.faceModel = faceModel;
        }
    }
}
