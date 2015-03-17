using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Images
{
    /// <summary>
    /// Enum to specify which compression scheme we have used for our frame
    /// </summary>
    public enum FrameCompressionType : int
    {
        /// <summary>
        /// Plain frame, no compression
        /// </summary>
        None = 0,
        /// <summary>
        /// Use snappy for compression
        /// </summary>
        Snappy = 1
    }
}
