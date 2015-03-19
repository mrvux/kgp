using KGP.Direct3D11;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP.Direct3D11.Tests
{
    [TestClass]
    public class BufferStrideTests
    {
        [TestMethod]
        public void ValidTest()
        {
            BufferStride count = new BufferStride(16);
            int value = count;
            Assert.AreEqual(value, 16);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroTest()
        {
            BufferStride count = new BufferStride(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NegativeTest()
        {
            BufferStride count = new BufferStride(-2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidModTest()
        {
            BufferStride count = new BufferStride(3);
        }

        [TestMethod]
        public void EqualityTest()
        {
            BufferStride count = new BufferStride(32);
            BufferStride count2 = new BufferStride(32);
            Assert.AreEqual(count, count2);
        }

        [TestMethod]
        public void EqualityTestInt()
        {
            BufferStride count = new BufferStride(32);
            Assert.AreEqual(count, 32);
        }

        [TestMethod]
        public void FromTest()
        {
            BufferStride s1 = new BufferStride(sizeof(float));
            BufferStride s2 = BufferStride.From<float>();
            Assert.AreEqual(s1, s2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFromTest()
        {
            BufferStride count = BufferStride.From<short>();
        }

    }
}
