using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Simple interface to provide a color frame
    /// </summary>
    public interface IColorRGBAFrameProvider
    {
        /// <summary>
        /// Raised when color frame is received
        /// </summary>
        event EventHandler<ColorRGBAFrameDataEventArgs> FrameReceived;
    }
}
