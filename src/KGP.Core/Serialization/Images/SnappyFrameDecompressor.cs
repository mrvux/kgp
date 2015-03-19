using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Images
{
    /// <summary>
    /// Allows to decompress a compressed frame back to kinect frame
    /// </summary>
    public unsafe class SnappyFrameDecompressor
    {
        private readonly KinectFrameInformation information;

        private byte[] unCompressedFrameData;

        /// <summary>
        /// Constructs a frame compressor
        /// </summary>
        /// <param name="information">Frame information</param>
        public SnappyFrameDecompressor(KinectFrameInformation information)
        {
            this.information = information;
            this.unCompressedFrameData = new byte[this.information.FrameDataSize];
        }

  
        /// <summary>
        /// Uncompress kinect frame data
        /// </summary>
        /// <param name="compressedData">Compressed data</param>
        /// <param name="compressedDataSize">Compressed data size</param>
        public void UnCompress(IntPtr compressedData, int compressedDataSize)
        {
            fixed(byte* bptr = &this.unCompressedFrameData[0])
            {
                int length = this.information.FrameDataSize;
                SnappyPI.SnappyCodec.Uncompress((byte*)compressedData, compressedDataSize, bptr, ref length);
            }
        }

        public static void Uncompress(IntPtr compressedData, int compressedDataSize, IntPtr target, int targetLength)
        {
            int length = targetLength;
            SnappyPI.SnappyCodec.Uncompress((byte*)compressedData, compressedDataSize, (byte*)target, ref length);
        }

        /// <summary>
        /// Compressed frame data
        /// </summary>
        public byte[] UnCompressedFrameData
        {
            get { return this.unCompressedFrameData; }
        }

        /// <summary>
        /// Compressed size
        /// </summary>
        public int UnCompressedSize
        {
            get { return this.information.FrameDataSize; }
        }
    }
}
