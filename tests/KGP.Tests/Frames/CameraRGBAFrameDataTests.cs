using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Frames;

namespace KGP.Tests
{
    [TestClass]
    public class CameraRGBAFrameDataTests
    {
        [TestMethod]
        public void TestConstrutor()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            bool pass = data.DataPointer != IntPtr.Zero;
            data.Dispose();
            Assert.AreEqual(true, pass);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDispose()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            data.Dispose();
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDisposeAccess()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            data.Dispose();

            //Should throw exception
            var pointer = data.DataPointer;
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestMultipleDispose()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            data.Dispose();
            //Second call to dispose should do nothing
            data.Dispose();
            
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        public void TestSize()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            int expected = 512 * 424 * 2;
            bool pass = data.SizeInBytes == expected;
            data.Dispose();
            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestDisposedSize()
        {
            CameraRGBAFrameData data = new CameraRGBAFrameData();
            data.Dispose();
            Assert.AreEqual(data.SizeInBytes, 0);
        }
    }
}
