using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Frames;

namespace KGP.Tests
{
    [TestClass]
    public class InfraredFrameDataTests
    {
        [TestMethod]
        public void TestConstrutor()
        {
            InfraredFrameData data = new InfraredFrameData();
            bool pass = data.DataPointer != IntPtr.Zero;
            data.Dispose();
            Assert.AreEqual(true, pass);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDispose()
        {
            InfraredFrameData data = new InfraredFrameData();
            data.Dispose();
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestDisposeAccess()
        {
            InfraredFrameData data = new InfraredFrameData();
            data.Dispose();

            //Should throw exception
            var pointer = data.DataPointer;
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestMultipleDispose()
        {
            InfraredFrameData data = new InfraredFrameData();
            data.Dispose();
            //Second call to dispose should do nothing
            data.Dispose();
            
            Assert.AreEqual(data.DataPointer, IntPtr.Zero);
        }

        [TestMethod]
        public void TestSize()
        {
            InfraredFrameData data = new InfraredFrameData();
            int expected = 512 * 424 * 2;
            bool pass = data.SizeInBytes == expected;
            data.Dispose();
            Assert.AreEqual(pass, true);
        }

        [TestMethod]
        public void TestDisposedSize()
        {
            InfraredFrameData data = new InfraredFrameData();
            data.Dispose();
            Assert.AreEqual(data.SizeInBytes, 0);
        }
    }
}
