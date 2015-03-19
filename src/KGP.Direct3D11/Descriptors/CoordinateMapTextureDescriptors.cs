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
    /// Utility class to get coordinate maps texture descriptors
    /// </summary>
    public static class CoordinateMapTextureDescriptors
    {
        /// <summary>
        /// Dynamic description for an rgba texture
        /// </summary>
        public static Texture2DDescription DynamicColorToDepth
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(new TextureSize(Consts.ColorWidth, Consts.ColorHeight), Format.R32G32_Float);
            }
        }

        /// <summary>
        /// Immutable description for rgba texture
        /// </summary>
        public static Texture2DDescription DynamicDepthToColor
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(new TextureSize(Consts.DepthWidth, Consts.DepthHeight), Format.R32G32_Float);
            }
        }
    }
}
