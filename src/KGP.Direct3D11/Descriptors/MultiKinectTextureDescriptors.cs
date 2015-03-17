using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    public static class MultiKinectTextureDescriptors
    {
        public static Texture2DDescription CameraTexture(int kinectCount)
        {
            return DescriptorUtils.GetRenderTargetTextureArray(Consts.DepthWidth, Consts.DepthHeight, SharpDX.DXGI.Format.R32G32B32A32_Float, kinectCount);
        }

        public static RenderTargetViewDescription CameraRenderTarget(int index)
        {
            return new RenderTargetViewDescription()
            {
                Dimension = RenderTargetViewDimension.Texture2DArray,
                Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                Texture2DArray = new RenderTargetViewDescription.Texture2DArrayResource()
                {
                    ArraySize = 1,
                    FirstArraySlice = index,
                    MipSlice = 0
                }
            };
        }

        public static Texture2DDescription BodyIndexTexture(int kinectCount)
        {
            return DescriptorUtils.GetRenderTargetTextureArray(Consts.DepthWidth, Consts.DepthHeight, SharpDX.DXGI.Format.R8_Typeless, kinectCount);
        }

        
    }
}
