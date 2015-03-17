using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Images
{
    /// <summary>
    /// Kinect frame information, used for compression
    /// </summary>
    public class KinectFrameInformation
    {
        private readonly int width;
        private readonly int height;
        private readonly int bitdepth;

        /// <summary>
        /// Frame width
        /// </summary>
        public int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// Frame height
        /// </summary>
        public int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Frame Bit Depth
        /// </summary>
        public int BitDepth
        {
            get { return this.bitdepth; }
        }

        /// <summary>
        /// Size of a frame, in bytes
        /// </summary>
        public int FrameDataSize
        {
            get { return this.width * this.height * this.bitdepth; }
        }
        
        private KinectFrameInformation(int width, int height, int bitdepth)
        {
            this.width = width;
            this.height = height;
            this.bitdepth = bitdepth;
        }

        /// <summary>
        /// Preset for depth frame
        /// </summary>
        public static KinectFrameInformation DepthFrame
        {
            get
            {
                return new KinectFrameInformation(Consts.DepthWidth, Consts.DepthHeight, sizeof(ushort));
            }
        }

        /// <summary>
        /// Preset for infrared frame
        /// </summary>
        public static KinectFrameInformation InfraredFrame
        {
            get
            {
                return new KinectFrameInformation(Consts.DepthWidth, Consts.DepthHeight, sizeof(ushort));
            }
        }

        /// <summary>
        /// Preset for body Index frame
        /// </summary>
        public static KinectFrameInformation BodyIndexFrame
        {
            get
            {
                return new KinectFrameInformation(Consts.DepthWidth, Consts.DepthHeight, sizeof(byte));
            }
        }
    }
}
