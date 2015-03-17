using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization
{
    /// <summary>
    /// Header for a kinect frame, to allow 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KinectFrameHeader
    {
        /// <summary>
        /// Frame compressed length
        /// </summary>
        public uint Length;
        
        /// <summary>
        /// Frame type
        /// </summary>
        public KinectFrameType FrameType;

        /// <summary>
        /// Frame compression type
        /// </summary>
        public FrameCompressionType Compression;
        
        /// <summary>
        /// Frame width
        /// </summary>
        public uint Width;
        
        /// <summary>
        /// Frame height
        /// </summary>
        public uint Height;
    }
}
