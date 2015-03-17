using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Adapter for kinect body, which allows us to get body information from several sources (kinect or network).
    /// Class is immutable, so can be accessed fromseveral threads.
    /// <remarks>Activities, Appearance, Engaged and Expressions are not part of this class, since they are now in Face API.</remarks>
    /// </summary>
    public class KinectBody
    {
        private readonly FrameEdges clippedEdges;
        private readonly TrackingConfidence handLeftConfidence;
        private readonly HandState handLeftState;
        private readonly TrackingConfidence handRightConfidence;
        private readonly HandState handRightState;
        private readonly bool isRestricted;
        private readonly bool isTracked;
        private readonly IReadOnlyDictionary<JointType, JointOrientation> jointOrientations;
        private readonly IReadOnlyDictionary<JointType, Joint> joints;
        private readonly PointF lean;
        private readonly TrackingState leanTrackingState;
        private readonly ulong trackingId;

        /// <summary>
        /// Constructs a bdy adapter from a kinect sdk body
        /// </summary>
        /// <param name="body">Body from Kinect SDK</param>
        public KinectBody(Microsoft.Kinect.Body body)
        {
            this.clippedEdges = body.ClippedEdges;
            this.handLeftConfidence = body.HandLeftConfidence;
            this.handLeftState = body.HandLeftState;
            this.handRightConfidence = body.HandRightConfidence;
            this.handRightState = body.HandRightState;
            this.isRestricted = body.IsRestricted;
            this.isTracked = body.IsTracked;
            this.jointOrientations = body.JointOrientations;
            this.joints = body.Joints;
            this.lean = body.Lean;
            this.leanTrackingState = body.LeanTrackingState;
            this.trackingId = body.TrackingId;
        }

        /// <see cref="Microsoft.Kinect.Body.ClippedEdges"/>
        public FrameEdges ClippedEdges
        {
            get { return this.clippedEdges; }
        }

        /// <see cref="Microsoft.Kinect.Body.HandLeftConfidence"/>
        public TrackingConfidence HandLeftConfidence
        {
            get { return this.handLeftConfidence; }
        }

        /// <see cref="Microsoft.Kinect.Body.HandLeftState"/>
        public HandState HandLeftState
        {
            get { return this.handLeftState; }
        }

        /// <see cref="Microsoft.Kinect.Body.HandRightConfidence"/>
        public TrackingConfidence HandRightConfidence
        {
            get { return this.handRightConfidence; }
        }

        /// <see cref="Microsoft.Kinect.Body.HandRightState"/>
        public HandState HandRightState
        {
            get { return this.handRightState; }
        }

        /// <see cref="Microsoft.Kinect.Body.IsRestricted"/>
        public bool IsRestricted
        {
            get { return this.isRestricted; }
        }

        /// <see cref="Microsoft.Kinect.Body.IsTracked"/>
        public bool IsTracked
        {
            get { return this.isTracked; }
        }

        /// <see cref="Microsoft.Kinect.Body.JointOrientations"/>
        public IReadOnlyDictionary<JointType,JointOrientation> JointOrientations
        {
            get { return this.jointOrientations; }
        }

        /// <see cref="Microsoft.Kinect.Body.Joints"/>
        public IReadOnlyDictionary<JointType,Joint> Joints
        {
            get { return this.joints; }
        }

        /// <see cref="Microsoft.Kinect.Body.Lean"/>
        public PointF Lean
        {
            get { return this.lean; }
        }

        /// <see cref="Microsoft.Kinect.Body.LeanTrackingState"/>
        public TrackingState LeanTrackingState
        {
            get { return this.leanTrackingState; }
        }

        /// <see cref="Microsoft.Kinect.Body.TrackingId"/>
        public ulong TrackingId
        {
            get { return this.trackingId; }
        }
    }
}
