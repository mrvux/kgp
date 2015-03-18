using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Serialization.Body;
using KGP.Tests.Fakes;
using Microsoft.Kinect;

namespace KGP.Tests
{
    [TestClass]
    public class KinectBodyInternalTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullOrientations()
        {
            KinectBodyInternal body = FakeInternalBodies.NullOrientationsBody();
            body.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullJoints()
        {
            KinectBodyInternal body = FakeInternalBodies.NullJointsBody();
            body.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongJointsCount()
        {
            KinectBodyInternal body = FakeInternalBodies.WrongJointCount();
            body.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongJointsOrientationCount()
        {
            KinectBodyInternal body = FakeInternalBodies.WrongJointOrientationCount();
            body.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateJointException))]
        public void TestDuplicateJointCount()
        {
            KinectBodyInternal body = FakeInternalBodies.DuplicateJoints();
            body.Validate();
        }

        [TestMethod]
        public void TestDuplicateHead()
        {
            KinectBodyInternal body = FakeInternalBodies.DuplicateHead();       
            try
            {
                body.Validate();
                Assert.Fail();      
            }
            catch (DuplicateJointException ex)
            {
                Assert.AreEqual(ex.Joint, JointType.Head);
            }
        }

        [TestMethod]
        public void TestValid()
        {
            KinectBodyInternal body = FakeInternalBodies.SimpleValidBody();
            body.Validate();
        }
    }
}
