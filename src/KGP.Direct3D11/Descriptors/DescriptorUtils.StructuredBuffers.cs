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
