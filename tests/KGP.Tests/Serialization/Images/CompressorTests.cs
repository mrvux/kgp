using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Frames;
using KGP.Tests.Fakes;
using KGP.Serialization.Images;

namespace KGP.Tests.Serialization.Images
{
    [TestClass]
    public unsafe class CompressorTests
    {
        [TestMethod]
        public void TextCompressBodyFrame()
        {
            DepthFrameData frame = FakeImageData.RandomDepthFrame();

            SnappyFrameCompressor compressor = new SnappyFrameCompressor(KinectFrameInformation.DepthFrame);
            compressor.Compress(frame.DataPointer);

            SnappyFrameDecompressor decompressor = new SnappyFrameDecompressor(KinectFrameInformation.DepthFrame);
            
            var barray = compressor.CompressedFrameData;
            fixed (byte* bptr = &barray[0])
            {
                decompressor.UnCompress(new IntPtr(bptr), compressor.CompressedSize);
            }


            var decomparray = decompressor.UnCompressedFrameData;
            fixed (byte* bptr = &barray[0])
            {
                FakeImageData.ByteCheck(frame.DataPointer, new IntPtr(bptr), decompressor.UnCompressedSize);
            }
            frame.Dispose();
            
        }
    }
}
