using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Interface to receive long exposure infrared frames
    /// </summary>
    public interface ILongExposureInfraredFrameProvider
    {
        /// <summary>
        /// Raised when long exposure infrared frame is received
        /// </summary>
        event EventHandler<LongExposureInfraredFrameDataEventArgs> FrameReceived;
    }
}
