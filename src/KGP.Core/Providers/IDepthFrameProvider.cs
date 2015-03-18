using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Simple interface to allow switching depth image provider
    /// </summary>
    public interface IDepthFrameProvider
    {
        /// <summary>
        /// Raised when depth frame is received
        /// </summary>
        event EventHandler<DepthFrameDataEventArgs> FrameReceived;
    }
}
