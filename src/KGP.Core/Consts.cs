using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Standard constants
    /// </summary>
    public class Consts
    {
        /// <summary>
        /// Width for Kinect2 depth image
        /// </summary>
        public const int DepthWidth = 512;

        /// <summary>
        /// Height for Kinect2 depth image
        /// </summary>
        public const int DepthHeight = 424;

        /// <summary>
        /// Total number of pixels for a depth camera frame
        /// </summary>
        public static readonly int DepthPixelCount = DepthWidth * DepthHeight;

        /// <summary>
        /// Width for Kinect2 RGB Image
        /// </summary>
        public const int ColorWidth = 1920;

        /// <summary>
        /// Height for Kinect2 RGB Image
        /// </summary>
        public const int ColorHeight = 1080;

        /// <summary>
        /// Maximum amount of bodies that kinect is able to track
        /// </summary>
        public const int MaxBodyCount = 6;

        /// <summary>
        /// Number of joints for kinect body
        /// </summary>
        public const int MaxJointCount = 25;
    }
}
