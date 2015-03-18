using KGP.Direct3D11.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP.Direct3D11.Tests
{
    [TestClass]
    public class DescriptorUtilsBuffersViewTests
    {
        [TestMethod]
        public void DynamicBufferDesc()
        {
            var desc = DescriptorUtils.DynamicStructuredBuffer(1024, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementDynamicBufferDesc()
        {
            var desc = DescriptorUtils.DynamicStructuredBuffer(0, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidStrideDynamicBufferDesc()
        {
            var desc = DescriptorUtils.DynamicStructuredBuffer(1024, 1);
        }

        [TestMethod]
        public void ImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(1024, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(0, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidStrideImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(1024, 1);
        }

        [TestMethod]
        public void WriteableBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(1024, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementWriteBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(0, 32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidStrideWriteBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(1024, 1);
        }
    }
}
