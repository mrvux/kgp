using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Providers;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Processors
{
    /// <summary>
    /// Simple wrapper to provide a color texture upload workflow
    /// </summary>
    public class DynamicColorRGBATextureProcessor : IDisposable
    {
        private readonly IColorRGBAFrameProvider colorFrameProvider;
        private ColorRGBAFrameData currentFrameData;
        private DynamicColorRGBATexture colorTexture;
        private bool upload;

        /// <summary>
        /// Color Texture
        /// </summary>
        public DynamicColorRGBATexture Texture
        {
            get { return this.colorTexture; }
        }

        /// <summary>
        /// Tells if our frame needs to be uploaded to gpu
        /// </summary>
        public bool NeedUpdate
        {
            get { return this.upload; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorFrameProvider">Color frame provider</param>
        /// <param name="device">Direct3D11 device</param>
        public DynamicColorRGBATextureProcessor(IColorRGBAFrameProvider colorFrameProvider, Device device)
        {
            if (colorFrameProvider == null)
                throw new ArgumentNullException("colorFrameProvider");
            if (device == null)
                throw new ArgumentNullException("device");

            this.colorFrameProvider = colorFrameProvider;
            this.colorFrameProvider.FrameReceived += FrameReceived;
            this.colorTexture = new DynamicColorRGBATexture(device);
        }

        private void FrameReceived(object sender, ColorRGBAFrameDataEventArgs e)
        {
            this.currentFrameData = e.FrameData;
            this.upload = true;
        }

        /// <summary>
        /// Copy last frame data to gpu, if applicable
        /// </summary>
        /// <param name="context">Device context</param>
        public void Update(DeviceContext context)
        {
            if (upload)
            {
                this.colorTexture.Copy(context, this.currentFrameData);
                upload = false;
            }
        }
        
        /// <summary>
        /// Disposes resources
        /// </summary>
        public void Dispose()
        {
            this.colorTexture.Dispose();
            this.colorFrameProvider.FrameReceived -= FrameReceived;       
        }
    }
}
