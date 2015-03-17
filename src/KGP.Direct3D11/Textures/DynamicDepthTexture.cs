using KGP.Direct3D11.Descriptors;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    public class DynamicDepthTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private ShaderResourceView normalizedView;

        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        public ShaderResourceView NormalizedView
        {
            get { return this.normalizedView; }
        }

        public DynamicDepthTexture(Device device)
        {
            this.texture = new Texture2D(device, DepthTextureDescriptors.DynamicResource);
            this.rawView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.RawView);
            this.normalizedView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.NormalizedView);
        }

        public void Copy(DeviceContext context, IntPtr dataPointer)
        {
            this.texture.Upload(context, dataPointer, Consts.DepthPixelCount * sizeof(short));
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.normalizedView.Dispose();
            this.texture.Dispose();
        }
    }
}
