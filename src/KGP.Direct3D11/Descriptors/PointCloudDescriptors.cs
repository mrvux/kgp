using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    /// <summary>
    /// Descriptors for point cloud buffers
    /// </summary>
    public static class PointCloudDescriptors
    {
        public static BufferDescription MultiKinectBuffer(int kinectCount, int stride)
        {
            return DescriptorUtils.WriteableStructuredBuffer(Consts.DepthPixelCount * kinectCount, stride);
        }

        public static BufferDescription Buffer(int stride)
        {
            return DescriptorUtils.WriteableStructuredBuffer(Consts.DepthPixelCount, stride);
        }

        public static BufferDescription PositionBuffer
        {
            get
            {
                return Buffer(Marshal.SizeOf(typeof(Vector3)));
            }
        }

        public static UnorderedAccessViewDescription AppendView
        {
            get
            {
                return DescriptorUtils.AppendStructuredBufferView(Consts.DepthPixelCount);
            }
        }

        public static UnorderedAccessViewDescription CounterView
        {
            get
            {
                return DescriptorUtils.CounterStructuredBufferView(Consts.DepthPixelCount);
            }
        }

        public static UnorderedAccessViewDescription WriteableView
        {
            get
            {
                return DescriptorUtils.WriteStructuredBufferView(Consts.DepthPixelCount);
            }
        }
    }
}
