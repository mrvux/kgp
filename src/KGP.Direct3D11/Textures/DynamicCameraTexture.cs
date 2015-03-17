using KGP.Direct3D11.Descriptors;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    public class DynamicCameraTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;

        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        public DynamicCameraTexture(Device device)
        {
            this.texture = new Texture2D(device, CameraTextureDescriptors.DynamicRGBA);
            this.rawView = new ShaderResourceView(device, this.texture);
        }

        public void Copy(DeviceContext context, IntPtr dataPointer)
        {
            this.texture.Upload(context, dataPointer, Consts.DepthPixelCount * Marshal.SizeOf(typeof(Vector4)));
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
        }
    }
}
