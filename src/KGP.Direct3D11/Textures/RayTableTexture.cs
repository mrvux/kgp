using KGP.Direct3D11.Descriptors;
using Microsoft.Kinect;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    public class RayTableTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;

        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        private RayTableTexture(Texture2D texture, ShaderResourceView view)
        {
            this.texture = texture;
            this.rawView = view;
        }

        public unsafe static RayTableTexture FromCoordinateMapper(Device device, CoordinateMapper coordinateMapper)
        {
            var points = coordinateMapper.GetDepthFrameToCameraSpaceTable();

            fixed (PointF* ptr = &points[0])
            {
                DataRectangle rect = new DataRectangle(new IntPtr(ptr), Consts.DepthWidth * 8);
                var texture = new Texture2D(device, LookupTableTextureDescriptors.DepthToCameraRayTable, rect);
                var view = new ShaderResourceView(device, texture);
                return new RayTableTexture(texture, view);
            }
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
        }
    }
}
