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
    /// Dynamic infrared texture
    /// </summary>
    public class ImmutableInfraredTexture : IKinectInfraredTexture, IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView shaderView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.shaderView; }
        }

        /// <summary>
        /// Creates a dynamic depth texture, allocates GPU resources
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        /// <param name="frameData">Initial frame data</param>
        public ImmutableInfraredTexture(Device device, InfraredFrameData frameData)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (frameData == null)
                throw new ArgumentNullException("frameData");

            DataRectangle rect = new DataRectangle(frameData.DataPointer, Consts.DepthWidth * sizeof(ushort));

            this.texture = new Texture2D(device, InfraredTextureDescriptors.DynamicResource,rect);
            this.shaderView = new ShaderResourceView(device, this.texture);
        }

        /// <summary>
        /// Copy depth frame to graphics card
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="data">Infrared frame data</param>
        public void Copy(DeviceContext context, InfraredFrameData data)
        {
            this.texture.Upload(context, data.DataPointer, data.SizeInBytes);
        }

        /// <summary>
        /// Disposes gpu resources
        /// </summary>
        public void Dispose()
        {
            this.shaderView.Dispose();
            this.texture.Dispose();
        }
    }
}
