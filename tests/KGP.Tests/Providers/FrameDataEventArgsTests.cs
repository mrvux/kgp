using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Frames;
using KGP.Providers;

namespace KGP.Tests.Providers
{
    [TestClass]
    public class FrameDataEventArgsTests
    {
        [TestMethod]
        public void DepthTestConstructor()
        {
            using (DepthFrameData depthFrame = new DepthFrameData())
            {
                DepthFrameDataEventArgs args = new DepthFrameDataEventArgs(depthFrame);
                Assert.AreEqual(depthFrame, args.DepthData);
            }  
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DepthTestNull()
        {
            DepthFrameDataEventArgs args = new DepthFrameDataEventArgs(null);
        }

        [TestMethod]
        public void BodyIndexTestConstructor()
        {
            using (BodyIndexFrameData frame = new BodyIndexFrameData())
            {
                BodyIndexFrameDataEventArgs args = new BodyIndexFrameDataEventArgs(frame);
                Assert.AreEqual(frame, args.FrameData);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BodyIndexTestNull()
        {
            BodyIndexFrameDataEventArgs args = new BodyIndexFrameDataEventArgs(null);
        }

        [TestMethod]
        public void ColorRGBATestConstructor()
        {
            using (ColorRGBAFrameData frame = new ColorRGBAFrameData())
            {
                ColorRGBAFrameDataEventArgs args = new ColorRGBAFrameDataEventArgs(frame);
                Assert.AreEqual(frame, args.FrameData);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ColorRGBATestNull()
        {
            ColorRGBAFrameDataEventArgs args = new ColorRGBAFrameDataEventArgs(null);
        }

        [TestMethod]
        public void KinectBodyTestValid()
        {
            KinectBody[] data = new KinectBody[]
            {
                Fakes.FakeBodies.FakeRandomBody(128,true),
                Fakes.FakeBodies.FakeRandomBody(129,true)
            };
            
            KinectBodyFrameDataEventArgs args = new KinectBodyFrameDataEventArgs(data);

            Assert.AreEqual(args.FrameData[0], data[0]);
            Assert.AreEqual(args.FrameData[1], data[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void KinectBodyTestNull()
        {
            KinectBodyFrameDataEventArgs args = new KinectBodyFrameDataEventArgs(null);
        }
    }
}
