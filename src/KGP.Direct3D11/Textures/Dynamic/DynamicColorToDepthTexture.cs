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
    /// Data holder for dynamic color to depth texture map
    /// </summary>
    public class DynamicColorToDepthTexture : IDisposable
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
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        public DynamicColorToDepthTexture(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.texture = new Texture2D(device, CoordinateMapTextureDescriptors.DynamicColorToDepth);
            this.shaderView = new ShaderResourceView(device, this.texture);
        }

        /// <summary>
        /// Copy data fromcpu to gpu
        /// <remarks>In that case we should use immediate context, do not use a deffered context 
        /// unless you really know what you do</remarks>
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="frame">Frame data</param>
        public void Copy(DeviceContext context, ColorToDepthFrameData frame)
        {
            this.texture.Upload(context, frame.DataPointer, frame.SizeInBytes);
        }

        /// <summary>
        /// Disposes views and texture
        /// </summary>
        public void Dispose()
        {
            this.shaderView.Dispose();
            this.texture.Dispose();
        }
    }
}
