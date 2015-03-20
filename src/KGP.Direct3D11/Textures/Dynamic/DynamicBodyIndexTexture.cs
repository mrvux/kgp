using KGP.Direct3D11.Descriptors;
using KGP.Frames;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Data holder for dynamic body index texture
    /// </summary>
    public class DynamicBodyIndexTexture : IKinectBodyIndexTexture, IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private ShaderResourceView normalizedView;

        /// <summary>
        /// Raw shader view, use as typed resource
        /// </summary>
        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Normalized view, use with a sampler
        /// </summary>
        public ShaderResourceView NormalizedView
        {
            get { return this.normalizedView; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        public DynamicBodyIndexTexture(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.texture = new Texture2D(device, BodyIndexTextureDescriptors.DynamicResource);
            this.rawView = new ShaderResourceView(device, this.texture, BodyIndexTextureDescriptors.RawView);
            this.normalizedView = new ShaderResourceView(device, this.texture, BodyIndexTextureDescriptors.NormalizedView);
        }

        /// <summary>
        /// Copy body index data fromcpu to gpu
        /// <remarks>In that case we should use immediate context, do not use a deffered context 
        /// unless you really know what you do</remarks>
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="frame">Frame data</param>
        public void Copy(DeviceContext context, BodyIndexFrameData frame)
        {
            this.texture.Upload(context, frame.DataPointer, frame.SizeInBytes);
        }

        /// <summary>
        /// Disposes views and texture
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.normalizedView.Dispose();
            this.texture.Dispose();
        }
    }
}
