using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests.Fakes
{
    public static class FakeBodies
    {
        public static KinectBody FakeRandomBody(ulong id, bool tracked)
        {
            return new KinectBody(FakeInternalBodies.RandomBody(id, tracked));
        }

        public static KinectBody FakeSpineBody(ulong id,bool tracked, CameraSpacePoint cp)
        {
            return new KinectBody(FakeInternalBodies.RandomBodySpine(id, tracked, cp));
        }

        public static KinectBody BodyWithRightHandState(ulong id, TrackingConfidence confidence, HandState state)
        {
            return new KinectBody(FakeInternalBodies.BodyWithRightHandState(id, confidence, state));
        }

        public static KinectBody BodyWithLeftHandState(ulong id, TrackingConfidence confidence, HandState state)
        {
            return new KinectBody(FakeInternalBodies.BodyWithLeftHandState(id, confidence, state));
        }
    }
}
