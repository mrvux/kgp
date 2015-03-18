using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Frames;

namespace KGP.Tests
{
    [TestClass]
    public class ColorRGBAFrameDataTests
    {
        [TestMethod]
        public void TestConstrutor()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            bool pass = data.DataPointer != IntPtr.Zero;
            data.Dispose();
            Assert.AreEqual(true, pass);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDispose()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            data.Dispose();
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDisposeAccess()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            data.Dispose();

            //Should throw exception
            var pointer = data.DataPointer;
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestMultipleDispose()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            data.Dispose();
            //Second call to dispose should do nothing
            data.Dispose();
            
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        public void TestSize()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            int expected = 1920*1080*4;
            bool pass = data.SizeInBytes == expected;
            data.Dispose();
            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestDisposedSize()
        {
            ColorRGBAFrameData data = new ColorRGBAFrameData();
            data.Dispose();
            Assert.AreEqual(data.SizeInBytes, 0);
        }
    }
}
