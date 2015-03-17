using KGP.Direct3D11.Descriptors;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    public class DynamicBodyIndexTexture : IDisposable
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

        public DynamicBodyIndexTexture(Device device)
        {
            this.texture = new Texture2D(device, BodyIndexTextureDescriptors.DynamicResource);
            this.rawView = new ShaderResourceView(device, this.texture, BodyIndexTextureDescriptors.RawView);
            this.normalizedView = new ShaderResourceView(device, this.texture, BodyIndexTextureDescriptors.NormalizedView);
        }

        public void Copy(DeviceContext context, IntPtr dataPointer)
        {
            this.texture.Upload(context, dataPointer, Consts.DepthPixelCount * sizeof(byte));
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.normalizedView.Dispose();
            this.texture.Dispose();
        }
    }
}
