using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    /// <summary>
    /// Utility class to get color texture descriptors
    /// </summary>
    public static class ColorTextureDescriptors
    {
        /// <summary>
        /// Dynamic description for an rgba texture
        /// </summary>
        public static Texture2DDescription DynamicRGBAResource
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(new TextureSize(Consts.ColorWidth, Consts.ColorHeight), Format.R8G8B8A8_UNorm);
            }
        }

        /// <summary>
        /// Immutable description for rgba texture
        /// </summary>
        public static Texture2DDescription ImmutableRGBAResource
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(new TextureSize(Consts.ColorWidth, Consts.ColorHeight), Format.R8G8B8A8_UNorm);
            }
        }

        /// <summary>
        /// Render target description for rgba texture. Can be used if we run yuv->rgba conversion kernel in gpu.
        /// </summary>
        public static Texture2DDescription RenderTargetRGBAResource
        {
            get
            {
                return DescriptorUtils.GetRenderTargetTexture(new TextureSize(Consts.ColorWidth, Consts.ColorHeight), Format.R8G8B8A8_UNorm);
            }
        }
    }
}
