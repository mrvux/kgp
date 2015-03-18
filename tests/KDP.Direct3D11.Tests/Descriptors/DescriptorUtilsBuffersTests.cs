using KGP.Direct3D11;
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
            var desc = DescriptorUtils.DynamicStructuredBuffer(new BufferElementCount(1024), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementDynamicBufferDesc()
        {
            var desc = DescriptorUtils.DynamicStructuredBuffer(new BufferElementCount(0), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidStrideDynamicBufferDesc()
        {
            var desc = DescriptorUtils.DynamicStructuredBuffer(new BufferElementCount(1024), new BufferStride(1));
        }

        [TestMethod]
        public void ImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(new BufferElementCount(1024), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(new BufferElementCount(0), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidStrideImmutableBufferDesc()
        {
            var desc = DescriptorUtils.ImmutableStructuredBuffer(new BufferElementCount(1024), new BufferStride(1));
        }

        [TestMethod]
        public void WriteableBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(new BufferElementCount(1024), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementWriteBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(new BufferElementCount(0), new BufferStride(32));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidStrideWriteBufferDesc()
        {
            var desc = DescriptorUtils.WriteableStructuredBuffer(new BufferElementCount(1024), new BufferStride(1));
        }
    }
}
