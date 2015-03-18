using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Descriptors;

namespace KDP.Direct3D11.Tests.Descriptors
{
    [TestClass]
    public class MultiKinectTextureDescriptorsTests
    {
        [TestMethod]
        public void TestCameraTextureValid()
        {
            var desc = MultiKinectTextureDescriptors.CameraTexture(2);

            Assert.AreEqual(desc.ArraySize, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCameraTextureInvalid()
        {
            var desc = MultiKinectTextureDescriptors.CameraTexture(0);
        }

        [TestMethod]
        public void TestBodyIndexTextureValid()
        {
            var desc = MultiKinectTextureDescriptors.BodyIndexTexture(2);

            Assert.AreEqual(desc.ArraySize, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBodyIndexTextureInValid()
        {
            var desc = MultiKinectTextureDescriptors.BodyIndexTexture(0);
        }

        [TestMethod]
        public void TestRenderTargetValid()
        {
            var desc = MultiKinectTextureDescriptors.CameraRenderTarget(2);
        }

        [TestMethod]
        public void TestRenderTargetZeroValid()
        {
            var desc = MultiKinectTextureDescriptors.CameraRenderTarget(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRenderTargetInValid()
        {
            var desc = MultiKinectTextureDescriptors.CameraRenderTarget(-2);
        }
    }
}
