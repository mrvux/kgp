using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Interface to implement for any texture that represents a kinect depth texture.
    /// Any implementers must guarantee texture size and format
    /// </summary>
    public interface IKinectDepthTexture
    {
        /// <summary>
        /// Raw view, must be of R16_UInt format
        /// </summary>
        ShaderResourceView RawView { get; }

        /// <summary>
        /// Normalized View, must be of R16_UNorm format
        /// </summary>
        ShaderResourceView NormalizedView { get; }
    }
}
