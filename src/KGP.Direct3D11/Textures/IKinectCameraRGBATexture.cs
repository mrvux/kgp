using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Commond interface for camera grba texture view
    /// </summary>
    public interface IKinectCameraRGBATexture
    {
        /// <summary>
        /// Shader resource view
        /// </summary>
        ShaderResourceView ShaderView { get; }
    }
}
