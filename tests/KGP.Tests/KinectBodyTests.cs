using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Serialization.Body;
using KGP.Tests.Fakes;
using Microsoft.Kinect;

namespace KGP.Tests
{
    [TestClass]
    public class KinectBodyTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullOrientations()
        {
            KinectBodyInternal body = FakeInternalBodies.NullOrientationsBody();
            KinectBody kb = new KinectBody(body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullJoints()
        {
            KinectBodyInternal body = FakeInternalBodies.NullJointsBody();
            KinectBody kb = new KinectBody(body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongJointsCount()
        {
            KinectBodyInternal body = FakeInternalBodies.WrongJointCount();
            KinectBody kb = new KinectBody(body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongJointsOrientationCount()
        {
            KinectBodyInternal body = FakeInternalBodies.WrongJointOrientationCount();
            KinectBody kb = new KinectBody(body);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateJointException))]
        public void TestDuplicateJointCount()
        {
            KinectBodyInternal body = FakeInternalBodies.DuplicateJoints();
            KinectBody kb = new KinectBody(body);
        }

        [TestMethod]
        public void TestDuplicateHead()
        {
            KinectBodyInternal body = FakeInternalBodies.DuplicateHead();
            try
            {
                KinectBody kb = new KinectBody(body);
            }
            catch (DuplicateJointException ex)
            {
                Assert.AreEqual(ex.Joint, JointType.Head);
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestValid()
        {
            KinectBodyInternal body = FakeInternalBodies.SimpleValidBody();
            KinectBody kb = new KinectBody(body);

            Assert.AreEqual(body.ClippedEdges, kb.ClippedEdges);
            Assert.AreEqual(body.HandLeftConfidence, kb.HandLeftConfidence);
            Assert.AreEqual(body.HandLeftState, kb.HandLeftState);
            Assert.AreEqual(body.HandRightConfidence, kb.HandRightConfidence);
            Assert.AreEqual(body.HandRightState, kb.HandRightState);
            Assert.AreEqual(body.IsRestricted, kb.IsRestricted);
            Assert.AreEqual(body.IsTracked, kb.IsTracked);
            Assert.AreEqual(body.Lean, kb.Lean);
            Assert.AreEqual(body.LeanTrackingState, kb.LeanTrackingState);
            Assert.AreEqual(body.TrackingId, kb.TrackingId);
        }
    }
}
