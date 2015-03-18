using Microsoft.Kinect;
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
    /// Descriptor for joint buffers
    /// </summary>
    public static class JointBufferDescriptor
    {
        private static BufferElementCount MaxBodyJoints
        {
            get { return new BufferElementCount(Consts.MaxBodyCount * Microsoft.Kinect.Body.JointCount); } //We make sure our buffer is always big enough
        }

        /// <summary>
        /// Get a buffer description for a single kinect, number of elements = number of pixels in depth frame
        /// </summary>
        /// <param name="stride">Element stride, in bytes</param>
        /// <returns></returns>
        public static BufferDescription DynamicBuffer(BufferStride stride)
        {
            return DescriptorUtils.DynamicStructuredBuffer(MaxBodyJoints, stride);
        }

        /// <summary>
        /// Preset for color space joints
        /// </summary>
        public static BufferDescription ColorSpacePositionBuffer
        {
            get
            {
                return DynamicBuffer(new BufferStride(Marshal.SizeOf(typeof(ColorSpacePoint))));
            }      
        }

        /// <summary>
        /// Preset for camera space position
        /// </summary>
        public static BufferDescription CameraSpacePositionBuffer
        {
            get
            {
                return DynamicBuffer(new BufferStride(Marshal.SizeOf(typeof(CameraSpacePoint))));
            }
        }

        /// <summary>
        /// Preset for Body id
        /// </summary>
        public static BufferDescription IdBuffer
        {
            get
            {
                return DynamicBuffer(new BufferStride(sizeof(uint)));
            }
        }
    }
}
