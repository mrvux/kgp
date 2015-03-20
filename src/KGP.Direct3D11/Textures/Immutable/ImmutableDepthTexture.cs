using KGP.Direct3D11.Descriptors;
using KGP.Frames;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Immutable depth texture
    /// </summary>
    public class ImmutableDepthTexture : IKinectDepthTexture, IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private ShaderResourceView normalizedView;

        /// <summary>
        /// Raw shader view
        /// </summary>
        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Normalized shader view
        /// </summary>
        public ShaderResourceView NormalizedView
        {
            get { return this.normalizedView; }
        }

        /// <summary>
        /// Creates an immutable depth texture, allocates GPU resources
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        public ImmutableDepthTexture(Device device, DepthFrameData data)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (data == null)
                throw new ArgumentNullException("data");

            DataRectangle dr = new DataRectangle(data.DataPointer, Consts.DepthWidth * sizeof(ushort));

            this.texture = new Texture2D(device, DepthTextureDescriptors.ImmutableResource, dr);
            this.rawView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.RawView);
            this.normalizedView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.NormalizedView);
        }

        /// <summary>
        /// Disposes gpu resources
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.normalizedView.Dispose();
            this.texture.Dispose();
        }
    }
}
