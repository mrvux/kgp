using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Images
{
    /// <summary>
    /// Allows to compress kinect frame data
    /// </summary>
    public unsafe class SnappyFrameCompressor
    {
        private readonly KinectFrameInformation information;   
  
        private int maxCompressedLength;
  
        private byte[] compressedFrameData;
        private int compressedSize;

        /// <summary>
        /// Constructs a frame compressor
        /// </summary>
        /// <param name="information">Frame information</param>
        public SnappyFrameCompressor(KinectFrameInformation information)
        {
            this.information = information;
            this.maxCompressedLength = SnappyPI.SnappyCodec.GetMaximumCompressedLength(information.FrameDataSize);
            this.compressedFrameData = new byte[this.maxCompressedLength];
        }

        /// <summary>
        /// Compresses frame data
        /// </summary>
        /// <param name="frameData">Raw frame data</param>
        public void Compress(IntPtr frameData)
        {
            fixed(byte* bptr = &this.compressedFrameData[0])
            {
                this.compressedSize = this.maxCompressedLength;
                SnappyPI.SnappyCodec.Compress((byte*)frameData, this.information.FrameDataSize, bptr, ref this.compressedSize);
            }
        }

        /// <summary>
        /// Compressed frame data
        /// </summary>
        public byte[] CompressedFrameData
        {
            get { return this.compressedFrameData; }
        }

        /// <summary>
        /// Compressed size
        /// </summary>
        public int CompressedSize
        {
            get { return this.compressedSize; }
        }
    }
}
