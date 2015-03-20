using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Interface to provide a Kinect body index texture
    /// </summary>
    public interface IKinectBodyIndexTexture
    {
        /// <summary>
        /// Raw view, must be of format R8_UInt
        /// </summary>
        ShaderResourceView RawView { get; }

        /// <summary>
        /// Normalized view, must be of format R8_UNorm
        /// </summary>
        ShaderResourceView NormalizedView { get; }
    }
}
