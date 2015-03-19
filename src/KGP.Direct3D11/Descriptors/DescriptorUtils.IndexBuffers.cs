using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    public static partial class DescriptorUtils
    {
        /// <summary>
        /// Builds an immutable index buffer description with uint backend
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <returns>Buffer description to create index buffer</returns>
        public static BufferDescription ImmutableIndexBufferUint(BufferElementCount elementCount)
        {
            return new BufferDescription()
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = elementCount * sizeof(uint),
                StructureByteStride = sizeof(uint),
                Usage = ResourceUsage.Immutable
            };
        }

        /// <summary>
        /// Builds an immutable index buffer description with ushort backend
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <returns>Buffer description to create index buffer</returns>
        public static BufferDescription ImmutableIndexBufferUShort(BufferElementCount elementCount)
        {
            return new BufferDescription()
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = elementCount * sizeof(ushort),
                StructureByteStride = sizeof(ushort),
                Usage = ResourceUsage.Immutable
            };
        }
    }
}
