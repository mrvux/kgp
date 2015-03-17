using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization
{
    /// <summary>
    /// Enum to notify which kinect frame is compressed
    /// </summary>
    public enum KinectFrameType : int
    {
        /// <summary>
        /// Kinect depth frame
        /// </summary>
        Depth = 1,
        /// <summary>
        /// Kinect Body index frame
        /// </summary>
        BodyIndex = 2
    }
}
