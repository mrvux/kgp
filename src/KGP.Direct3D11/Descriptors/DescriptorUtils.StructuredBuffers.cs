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
        /// Builds a dynamic buffer description
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <param name="stride">Structure stride, in bytes</param>
        /// <returns>Buffer description to create structured buffer</returns>
        public static BufferDescription DynamicStructuredBuffer(int elementCount, int stride)
        {
            return new BufferDescription()
            {
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.BufferStructured,
                SizeInBytes = elementCount * stride,
                StructureByteStride = stride,
                Usage = ResourceUsage.Dynamic
            };
        }

        /// <summary>
        /// Builds an immutable buffer description
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <param name="stride">Structure stride, in bytes</param>
        /// <returns>Buffer description to create structured buffer</returns>
        public static BufferDescription ImmutableStructuredBuffer(int elementCount, int stride)
        {
            return new BufferDescription()
            {
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.BufferStructured,
                SizeInBytes = elementCount * stride,
                StructureByteStride = stride,
                Usage = ResourceUsage.Immutable
            };
        }

        /// <summary>
        /// Builds a writeable buffer description, that can be used as unordered access
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <param name="stride">Structure stride, in bytes</param>
        /// <returns>Buffer description to create structured buffer</returns>
        public static BufferDescription WriteableStructuredBuffer(int elementCount, int stride)
        {
            return new BufferDescription()
            {
                BindFlags = BindFlags.ShaderResource | BindFlags.UnorderedAccess,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.BufferStructured,
                SizeInBytes = elementCount * stride,
                StructureByteStride = stride,
                Usage = ResourceUsage.Default
            };
        }

        /// <summary>
        /// Builds a standard write unordered view description for a structured buffer
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <returns>View decription</returns>
        public static UnorderedAccessViewDescription WriteStructuredBufferView(int elementCount)
        {
            return new UnorderedAccessViewDescription()
            {
                Format = SharpDX.DXGI.Format.Unknown,
                Dimension = UnorderedAccessViewDimension.Buffer,
                Buffer = new UnorderedAccessViewDescription.BufferResource()
                {
                    ElementCount = elementCount,
                    FirstElement = 0,
                    Flags = UnorderedAccessViewBufferFlags.None
                }
            };
        }

        /// <summary>
        /// Builds an appendable view, so buffer can be used as Append/Consume buffer
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <returns>View decription</returns>
        public static UnorderedAccessViewDescription AppendStructuredBufferView(int elementCount)
        {
            return new UnorderedAccessViewDescription()
            {
                Format = SharpDX.DXGI.Format.Unknown,
                Dimension = UnorderedAccessViewDimension.Buffer,
                Buffer = new UnorderedAccessViewDescription.BufferResource()
                {
                    ElementCount = elementCount,
                    FirstElement = 0,
                    Flags = UnorderedAccessViewBufferFlags.Append
                }
            };
        }

        /// <summary>
        /// Builds a counter view, so buffer can be used with IncrementCounter
        /// </summary>
        /// <param name="elementCount">Element count</param>
        /// <returns>View decription</returns>
        public static UnorderedAccessViewDescription CounterStructuredBufferView(int elementCount)
        {
            return new UnorderedAccessViewDescription()
            {
                Format = SharpDX.DXGI.Format.Unknown,
                Dimension = UnorderedAccessViewDimension.Buffer,
                Buffer = new UnorderedAccessViewDescription.BufferResource()
                {
                    ElementCount = elementCount,
                    FirstElement = 0,
                    Flags = UnorderedAccessViewBufferFlags.Counter
                }
            };
        }
    }
}
