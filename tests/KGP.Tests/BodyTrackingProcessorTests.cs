using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Serialization.Body;
using KGP.Tests.Fakes;
using Microsoft.Kinect;
using KGP.Processors;
using System.Collections.Generic;

namespace KGP.Tests
{
    [TestClass]
    public class BodyTrackingProcessorTests
    {
        [TestMethod]
        public void TestTrackingFirstRaised()
        {
            bool isRaised = false;
            BodyTrackingProcessor processor = new BodyTrackingProcessor();
            processor.BodyTrackingStarted += (s, e) => isRaised = true;

            var dataSet = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true)
            };

            processor.Next(dataSet);

            Assert.AreEqual(isRaised, true);
        }

        [TestMethod]
        public void TestFirstFrame2in1out()
        {
            List<ulong> found = new List<ulong>();

            BodyTrackingProcessor processor = new BodyTrackingProcessor();
            processor.BodyTrackingStarted += (s, e) => found.Add(e.Body.TrackingId);

            var dataSet = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(256, true),
                FakeBodies.FakeRandomBody(32, false)
            };

            processor.Next(dataSet);

            Assert.AreEqual(found.Count, 2);
            Assert.AreEqual(found.Contains(128), true);
            Assert.AreEqual(found.Contains(256), true);
        }

        [TestMethod]
        public void TestSecondFrameNewBody()
        {
            List<ulong> found = new List<ulong>();

            BodyTrackingProcessor processor = new BodyTrackingProcessor();
            
            var firstFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(256, true),
                FakeBodies.FakeRandomBody(32, false)
            };

            processor.Next(firstFrame);

            //Register to event now
            processor.BodyTrackingStarted += (s, e) => found.Add(e.Body.TrackingId);

            ulong newBodyId = 17;

            var secondFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(newBodyId, true),
            };

            processor.Next(secondFrame);

            Assert.AreEqual(found.Count, 1);
            Assert.AreEqual(found.Contains(newBodyId), true);
        }

        [TestMethod]
        public void TestTrackingLostRaised()
        {
            List<ulong> found = new List<ulong>();

            BodyTrackingProcessor processor = new BodyTrackingProcessor();

            ulong lostBodyId = 256;

            var firstFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(lostBodyId, true),
                FakeBodies.FakeRandomBody(32, false)
            };

            processor.Next(firstFrame);

            //Register to event now
            processor.BodyTrackingLost += (s, e) => found.Add(e.Body.TrackingId);

            ulong newBodyId = 17;

            var secondFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(newBodyId, true),
            };

            processor.Next(secondFrame);

            Assert.AreEqual(found.Count, 1);
            Assert.AreEqual(found.Contains(lostBodyId), true);
        }

        [TestMethod]
        public void TestTrackingLostRaisedSetFalse()
        {
            List<ulong> found = new List<ulong>();

            BodyTrackingProcessor processor = new BodyTrackingProcessor();

            ulong lostBodyId = 256;

            var firstFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                FakeBodies.FakeRandomBody(lostBodyId, true),
                FakeBodies.FakeRandomBody(32, false)
            };

            processor.Next(firstFrame);

            //Register to event now
            processor.BodyTrackingLost += (s, e) => found.Add(e.Body.TrackingId);

            ulong newBodyId = 17;

            var secondFrame = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(128, true),
                 FakeBodies.FakeRandomBody(lostBodyId, false), //Set tracking state to false here
                FakeBodies.FakeRandomBody(newBodyId, true),
            };

            processor.Next(secondFrame);

            Assert.AreEqual(found.Count, 1);
            Assert.AreEqual(found.Contains(lostBodyId), true);
        }

    }
}
