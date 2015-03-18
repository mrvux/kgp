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
    public class HandTrackingProcessorTests
    {
        [TestMethod]
        public void TestTrackingLeftHandOpen()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Closed)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Open)
            };

            bool pass = false;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = args.Body == next[0] && args.HandType == HandType.Left && args.PreviousHandState == HandState.Closed;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }


        [TestMethod]
        public void TestTrackingLeftHandClose()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Open)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Closed)
            };

            bool pass = false;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = args.Body == next[0] && args.HandType == HandType.Left && args.PreviousHandState == HandState.Open;
            processor.Next(next);
            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestTrackingLeftHandNoChange()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Open)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithLeftHandState(128, TrackingConfidence.High, HandState.Open)
            };

            bool pass = true;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = false;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestTrackingRightHandOpen()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Closed)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Open)
            };

            bool pass = false;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = args.Body == next[0] && args.HandType == HandType.Right && args.PreviousHandState == HandState.Closed;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestTrackingRightHandClose()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Open)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Closed)
            };

            bool pass = false;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = args.Body == next[0] && args.HandType == HandType.Right && args.PreviousHandState == HandState.Open;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestTrackingRightHandNoChange()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Open)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Open)
            };

            bool pass = true;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = false;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void NewBodyDontRaiseEvent()
        {
            var initial = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(128, TrackingConfidence.High, HandState.Open)
            };

            var next = new KinectBody[]
            {
                FakeBodies.BodyWithRightHandState(142, TrackingConfidence.High, HandState.Closed)
            };

            bool pass = true;

            HandStateTrackingProcessor processor = new HandStateTrackingProcessor();
            processor.Next(initial);
            processor.HandStateChanged += (sender, args) => pass = false;
            processor.Next(next);

            Assert.AreEqual(pass, true);
        }


    }
}
