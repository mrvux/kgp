using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Kinect.Face;

namespace KGP.Tests
{
    [TestClass]
    public class FaceUtilsTests
    {
        [TestMethod]
        public void AllFeaturesTest()
        {
            var expected = FaceFrameFeatures.BoundingBoxInColorSpace
                | FaceFrameFeatures.PointsInColorSpace
                | FaceFrameFeatures.RotationOrientation
                | FaceFrameFeatures.FaceEngagement
                | FaceFrameFeatures.Glasses
                | FaceFrameFeatures.Happy
                | FaceFrameFeatures.LeftEyeClosed
                | FaceFrameFeatures.RightEyeClosed
                | FaceFrameFeatures.LookingAway
                | FaceFrameFeatures.MouthMoved
                | FaceFrameFeatures.MouthOpen;

            var sut = FaceUtils.AllFeatures();

            Assert.AreEqual(expected, sut);
        }
    }
}
