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
        /// <summary>
        /// Get a buffer description that can handle several kinects at once
        /// </summary>
        /// <param name="kinectCount">Kinect count</param>
        /// <param name="stride">Element stride, in bytes</param>
        /// <returns>Buffer description</returns>
        public static BufferDescription MultiKinectBuffer(int kinectCount, int stride)
        {
            return DescriptorUtils.WriteableStructuredBuffer(new BufferElementCount(Consts.DepthPixelCount * kinectCount), new BufferStride(stride));
        }

        /// <summary>
        /// Get a buffer description for a single kinect, number of elements = number of pixels in depth frame
        /// </summary>
        /// <param name="stride">Element stride, in bytes</param>
        /// <returns></returns>
        public static BufferDescription Buffer(int stride)
        {
            return DescriptorUtils.WriteableStructuredBuffer(new BufferElementCount(Consts.DepthPixelCount), new BufferStride(stride));
        }

        /// <summary>
        /// Utility to get a position buffer (Vector3 point cloud)
        /// </summary>
        public static BufferDescription PositionBuffer
        {
            get
            {
                return Buffer(Marshal.SizeOf(typeof(Vector3)));
            }
        }

        /// <summary>
        /// Gets an appendable view for our buffer
        /// </summary>
        public static UnorderedAccessViewDescription AppendView
        {
            get
            {
                return DescriptorUtils.AppendStructuredBufferView(new BufferElementCount(Consts.DepthPixelCount));
            }
        }

        /// <summary>
        /// Get a point cloud counter view
        /// </summary>
        public static UnorderedAccessViewDescription CounterView
        {
            get
            {
                return DescriptorUtils.CounterStructuredBufferView(new BufferElementCount(Consts.DepthPixelCount));
            }
        }

        /// <summary>
        /// Get a point cloud writeable view
        /// </summary>
        public static UnorderedAccessViewDescription WriteableView
        {
            get
            {
                return DescriptorUtils.WriteStructuredBufferView(new BufferElementCount(Consts.DepthPixelCount));
            }
        }
    }
}
