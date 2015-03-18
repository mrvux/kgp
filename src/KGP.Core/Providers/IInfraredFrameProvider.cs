using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Interface to receive infrared frames
    /// </summary>
    public interface IInfraredFrameProvider
    {
        /// <summary>
        /// Raised when infrared frame is received
        /// </summary>
        event EventHandler<InfraredFrameDataEventArgs> FrameReceived;
    }
}
