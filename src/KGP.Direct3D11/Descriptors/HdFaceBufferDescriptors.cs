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
    /// Descriptor for hd face index buffers
    /// </summary>
    public static class HdFaceBufferDescriptors
    {
        private static BufferElementCount MaxFaceIndices
        {
            get { return new BufferElementCount(Consts.MaxBodyCount * (int)Microsoft.Kinect.Face.FaceModel.TriangleCount * 3); } 
        }

        /// <summary>
        /// Get face indices index buffer immutable description
        /// </summary>
        /// <param name="faceCount">Face count</param>
        /// <returns>Description to create index buffer</returns>
        public static BufferDescription ImmutableIndexBuffer(int faceCount)
        {
            return DescriptorUtils.ImmutableIndexBufferUint(new BufferElementCount((int)Microsoft.Kinect.Face.FaceModel.TriangleCount * 3 * faceCount));
        }

        /// <summary>
        /// Get face indices structured buffer immutable description
        /// </summary>
        /// <param name="faceCount">Face count</param>
        /// <returns>Description for face structured buffer</returns>
        public static BufferDescription ImmutableIndexStructuredBuffer(int faceCount)
        {
            return DescriptorUtils.ImmutableStructuredBuffer(new BufferElementCount((int)Microsoft.Kinect.Face.FaceModel.TriangleCount * 3 * faceCount), BufferStride.From<uint>());
        }
    }
}
