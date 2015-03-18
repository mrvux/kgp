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
    public class BufferElementCountTests
    {
        [TestMethod]
        public void ValidTest()
        {
            BufferElementCount count = new BufferElementCount(25);
            int value = count;
            Assert.AreEqual(value, 25);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroTest()
        {
            BufferElementCount count = new BufferElementCount(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NegativeTest()
        {
            BufferElementCount count = new BufferElementCount(-2);
        }

        [TestMethod]
        public void StaticValidTest()
        {
            Assert.AreEqual(BufferElementCount.IsValid(32), true);
        }

        [TestMethod]
        public void ZeroValidTest()
        {
            Assert.AreEqual(BufferElementCount.IsValid(0), false);
        }

        [TestMethod]
        public void NegativeValidTest()
        {
            Assert.AreEqual(BufferElementCount.IsValid(-5), false);
        }

        [TestMethod]
        public void EqualityTest()
        {
            BufferElementCount count = new BufferElementCount(25);
            BufferElementCount count2 = new BufferElementCount(25);
            Assert.AreEqual(count, count2);
        }

        [TestMethod]
        public void EqualityTestInt()
        {
            BufferElementCount count = new BufferElementCount(25);
            Assert.AreEqual(count, 25);
        }
    }
}
