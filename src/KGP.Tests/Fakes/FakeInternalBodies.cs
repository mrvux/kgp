using KGP.Serialization.Body;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests.Fakes
{
    public static class FakeInternalBodies
    {
        public static KinectBodyInternal NullOrientationsBody()
        {
            return new KinectBodyInternal()
            {
                ClippedEdges = FrameEdges.None,
                HandLeftConfidence = TrackingConfidence.High,
                HandLeftState = HandState.NotTracked,
                HandRightConfidence = TrackingConfidence.High,
                HandRightState = HandState.NotTracked,
                IsRestricted = false,
                IsTracked = true,
                JointOrientations = null,
                Joints = FakeJoints.ValidRandomJoints(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }
    }
}
