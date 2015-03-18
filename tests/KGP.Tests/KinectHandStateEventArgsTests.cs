using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Tests.Fakes;

namespace KGP.Tests
{
    [TestClass]
    public class KinectHandStateEventArgsTests
    {
        [TestMethod]
        public void TestValidLeftOpen()
        {
            KinectBody body = FakeBodies.FakeRandomBody(128, true);
            var args = new KinectHandStateEventArgs(body, HandType.Left, Microsoft.Kinect.HandState.Open);

            Assert.AreEqual(body, args.Body);
            Assert.AreEqual(HandType.Left, args.HandType);
            Assert.AreEqual(Microsoft.Kinect.HandState.Open, args.PreviousHandState);
        }

        [TestMethod]
        public void TestValidLeftClosed()
        {
            KinectBody body = FakeBodies.FakeRandomBody(128, true);
            var args = new KinectHandStateEventArgs(body, HandType.Left, Microsoft.Kinect.HandState.Closed);

            Assert.AreEqual(body, args.Body);
            Assert.AreEqual(HandType.Left, args.HandType);
            Assert.AreEqual(Microsoft.Kinect.HandState.Closed, args.PreviousHandState);
        }

        [TestMethod]
        public void TestValidRightOpen()
        {
            KinectBody body = FakeBodies.FakeRandomBody(128, true);
            var args = new KinectHandStateEventArgs(body, HandType.Right, Microsoft.Kinect.HandState.Open);

            Assert.AreEqual(body, args.Body);
            Assert.AreEqual(HandType.Right, args.HandType);
            Assert.AreEqual(Microsoft.Kinect.HandState.Open, args.PreviousHandState);
        }

        [TestMethod]
        public void TestValidRightClosed()
        {
            KinectBody body = FakeBodies.FakeRandomBody(128, true);
            var args = new KinectHandStateEventArgs(body, HandType.Right, Microsoft.Kinect.HandState.Closed);

            Assert.AreEqual(body, args.Body);
            Assert.AreEqual(HandType.Right, args.HandType);
            Assert.AreEqual(Microsoft.Kinect.HandState.Closed, args.PreviousHandState);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            var args = new KinectHandStateEventArgs(null, HandType.Right, Microsoft.Kinect.HandState.Closed);
        }
    }
}
