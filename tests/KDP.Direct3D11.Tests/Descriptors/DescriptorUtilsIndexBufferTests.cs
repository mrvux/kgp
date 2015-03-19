using KGP.Direct3D11;
using KGP.Direct3D11.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP.Direct3D11.Tests
{
    [TestClass]
    public class DescriptorUtilsIndexBufferTests
    {
        [TestMethod]
        public void ImmutableTestUInt()
        {
            BufferElementCount count = new BufferElementCount(1024);
            var desc = DescriptorUtils.ImmutableIndexBufferUint(count);

            var expected = new BufferDescription()
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = count * sizeof(uint),
                StructureByteStride = sizeof(uint),
                Usage = ResourceUsage.Immutable
            };

            Assert.AreEqual(desc, expected);
        }

        [TestMethod]
        public void ImmutableTestUShort()
        {
            BufferElementCount count = new BufferElementCount(1024);
            var desc = DescriptorUtils.ImmutableIndexBufferUShort(count);

            var expected = new BufferDescription()
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = count * sizeof(ushort),
                StructureByteStride = sizeof(ushort),
                Usage = ResourceUsage.Immutable
            };

            Assert.AreEqual(desc, expected);
        }

    }
}
