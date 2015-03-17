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
    /// <summary>
    /// Multiple kinect camera texture. 
    /// </summary>
    public class MultiKinectCameraTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private RenderTargetView[] renderViews;

        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        public RenderTargetView GetRenderTargetView(int kinectIndex)
        {
            return renderViews[kinectIndex];
        }

        public MultiKinectCameraTexture(Device device, int kinectCount)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (kinectCount < 1)
                throw new ArgumentOutOfRangeException("kinectCount", kinectCount, "Kinect count should be at least one");

            this.texture = new Texture2D(device, MultiKinectTextureDescriptors.CameraTexture(kinectCount));
            this.rawView = new ShaderResourceView(device, this.texture);
            this.renderViews = new RenderTargetView[kinectCount];
            for (int i = 0; i < this.renderViews.Length; i++)
            {
                this.renderViews[i] = new RenderTargetView(device, this.texture, MultiKinectTextureDescriptors.CameraRenderTarget(i));
            }
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
            for (int i = 0; i < this.renderViews.Length; i++)
            {
                this.renderViews[i].Dispose();
            }
        }
    }
}
