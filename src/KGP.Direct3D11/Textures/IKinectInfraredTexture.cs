using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Interface for a kinect infrared texture view
    /// </summary>
    public interface IKinectInfraredTexture
    {
        /// <summary>
        /// Shader reource view
        /// </summary>
        ShaderResourceView ShaderView { get; }
    }
}
