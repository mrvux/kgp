using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Adapter for kinect body, which allows us to get body information from several sources (kinect or network)
    /// </summary>
    public class KinectBody
    {
        private readonly IReadOnlyDictionary<Activity, DetectionResult> activities;
        private readonly IReadOnlyDictionary<Appearance, DetectionResult> appearance;
        private readonly FrameEdges clippedEdges;
        private readonly DetectionResult engaged;
        private readonly IReadOnlyDictionary<Expression, DetectionResult> expressions;
        private readonly TrackingConfidence handLeftConfidence;
        private readonly HandState handLeftState;
        private readonly TrackingConfidence handRightConfidence;
        private readonly HandState handRightState;
        private readonly bool isRestricted;
        private readonly bool isTracked;
        private readonly int jointCount;
        private readonly IReadOnlyDictionary<JointType, JointOrientation> jointOrientations;
        private readonly IReadOnlyDictionary<JointType, Joint> joints;
        private readonly PointF lean;
        private readonly TrackingState leanTrackingState;
        private readonly ulong trackingId;

        public IReadOnlyDictionary<Activity, DetectionResult> Activities
        {
            get { return this.activities; }
        }

        public IReadOnlyDictionary<Appearance, DetectionResult> Appearance
        {
            get { return this.appearance; }
        }

        public FrameEdges ClippedEdges
        {
            get { return this.clippedEdges; }
        }

        public DetectionResult Engaged
        {
            get { return this.engaged; }
        }

        public IReadOnlyDictionary<Expression, DetectionResult> Expressions
        {
            get { return this.expressions; }
        }
        
        public TrackingConfidence HandLeftConfidence
        {
            get { return this.handLeftConfidence; }
        }

        public HandState HandLeftState
        {
            get { return this.handLeftState; }
        }

        public TrackingConfidence HandRightConfidence
        {
            get { return this.handRightConfidence; }
        }

        public HandState HandRightState
        {
            get { return this.handRightState; }
        }

        public bool IsRestricted
        {
            get { return this.isRestricted; }
        }

        public bool IsTracked
        {
            get { return this.isTracked; }
        }

        public int JointCount
        {
            get { return this.jointCount; }
        }

        public IReadOnlyDictionary<JointType,JointOrientation> JointOrientations
        {
            get { return this.jointOrientations; }
        }

        public IReadOnlyDictionary<JointType,Joint> Joints
        {
            get { return this.joints; }
        }

        public PointF Lean
        {
            get { return this.lean; }
        }

        public TrackingState LeanTrackingState
        {
            get { return this.leanTrackingState; }
        }

        public ulong TrackingId
        {
            get { return this.trackingId; }
        }
    }
}
