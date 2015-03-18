using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Interface to receive body frames
    /// </summary>
    public interface IBodyFrameProvider
    {
        /// <summary>
        /// Raised when body frame is received
        /// </summary>
        event EventHandler<KinectBodyFrameDataEventArgs> FrameReceived;
    }
}
