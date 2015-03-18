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
    public class DescriptorUtilsBuffersTests
    {
        [TestMethod]
        public void WriteView()
        {
            var desc = DescriptorUtils.WriteStructuredBufferView(new BufferElementCount(1024));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementWriteView()
        {
            var desc = DescriptorUtils.WriteStructuredBufferView(new BufferElementCount(0));
        }

        [TestMethod]
        public void AppendView()
        {
            var desc = DescriptorUtils.AppendStructuredBufferView(new BufferElementCount(1024));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementAppendView()
        {
            var desc = DescriptorUtils.AppendStructuredBufferView(new BufferElementCount(0));
        }

        [TestMethod]
        public void CounterView()
        {
            var desc = DescriptorUtils.CounterStructuredBufferView(new BufferElementCount(1024));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidElementCounterView()
        {
            var desc = DescriptorUtils.CounterStructuredBufferView(new BufferElementCount(0));
        }
    }
}
