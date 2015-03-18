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

        public static KinectBodyInternal NullJointsBody()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = null,
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal WrongJointCount()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = new Joint[Microsoft.Kinect.Body.JointCount+5], //Set a wrong joint count
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal WrongJointOrientationCount()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount+10],//Set a wrong joint count
                Joints = new Joint[Microsoft.Kinect.Body.JointCount], 
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal DuplicateJoints()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.DuplicateJoint(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal SimpleValidBody()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.ValidRandomJoints(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal DuplicateHead()
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
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.DuplicateHead(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = 0231
            };
        }

        public static KinectBodyInternal RandomBody(ulong id, bool isTracked)
        {
            return new KinectBodyInternal()
            {
                ClippedEdges = FrameEdges.None,
                HandLeftConfidence = TrackingConfidence.High,
                HandLeftState = HandState.NotTracked,
                HandRightConfidence = TrackingConfidence.High,
                HandRightState = HandState.NotTracked,
                IsRestricted = false,
                IsTracked = isTracked,
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.ValidRandomJoints(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = id
            };
        }

        public static KinectBodyInternal RandomBodySpine(ulong id, bool tracked, CameraSpacePoint cp)
        {
            var result = new KinectBodyInternal()
            {
                ClippedEdges = FrameEdges.None,
                HandLeftConfidence = TrackingConfidence.High,
                HandLeftState = HandState.NotTracked,
                HandRightConfidence = TrackingConfidence.High,
                HandRightState = HandState.NotTracked,
                IsRestricted = false,
                IsTracked = tracked,
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.ValidRandomJointsSpinePosition(cp),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = id
            };
            return result;
        }

        public static KinectBodyInternal BodyWithRightHandState(ulong id, TrackingConfidence confidence, HandState state)
        {
            var result = new KinectBodyInternal()
            {
                ClippedEdges = FrameEdges.None,
                HandLeftConfidence = TrackingConfidence.High,
                HandLeftState = HandState.NotTracked,
                HandRightConfidence = confidence,
                HandRightState = state,
                IsRestricted = false,
                IsTracked = true,
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.ValidRandomJoints(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = id
            };
            return result;
        }

        public static KinectBodyInternal BodyWithLeftHandState(ulong id, TrackingConfidence confidence, HandState state)
        {
            var result = new KinectBodyInternal()
            {
                ClippedEdges = FrameEdges.None,
                HandLeftConfidence = confidence,
                HandLeftState = state,
                HandRightConfidence = TrackingConfidence.High,
                HandRightState = HandState.NotTracked,
                IsRestricted = false,
                IsTracked = true,
                JointOrientations = new JointOrientation[Microsoft.Kinect.Body.JointCount],
                Joints = FakeJoints.ValidRandomJoints(),
                LeanTrackingState = TrackingState.NotTracked,
                TrackingId = id
            };
            return result;
        }
    }
}
