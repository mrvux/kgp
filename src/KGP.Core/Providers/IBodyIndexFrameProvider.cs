using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Interface to allow to swap how a body index frame is received
    /// </summary>
    public interface IBodyIndexFrameProvider
    {
        /// <summary>
        /// Raised when body index frame is received
        /// </summary>
        event EventHandler<BodyIndexFrameDataEventArgs> FrameReceived;
    }
}
