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
    /// <summary>
    /// Ray table texture, used to perform depth/world reconstruction in gpu
    /// </summary>
    public class RayTableTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        private RayTableTexture(Texture2D texture, ShaderResourceView view)
        {
            this.texture = texture;
            this.rawView = view;
        }

        /// <summary>
        /// Convenience factory to create table from Kinect coordinate mapper
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        /// <param name="coordinateMapper">Kinect Coordinate mapper</param>
        /// <returns>Ray table texture</returns>
        public unsafe static RayTableTexture FromCoordinateMapper(Device device, CoordinateMapper coordinateMapper)
        {
            var points = coordinateMapper.GetDepthFrameToCameraSpaceTable();
            return FromPoints(device, points);
        }

        /// <summary>
        /// Convenience factory to create table from Kinect coordinate mapper
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        /// <param name="initialData">Initial points array</param>
        /// <returns>Ray table texture</returns>
        public unsafe static RayTableTexture FromPoints(Device device, PointF[] initialData)
        {
            if (initialData.Length != Consts.DepthPixelCount)
                throw new ArgumentException("initialData", "Initial data length should be same size as depth frame pixel count");

            fixed (PointF* ptr = &initialData[0])
            {
                DataRectangle rect = new DataRectangle(new IntPtr(ptr), Consts.DepthWidth * 8);
                var texture = new Texture2D(device, LookupTableTextureDescriptors.DepthToCameraRayTable, rect);
                var view = new ShaderResourceView(device, texture);
                return new RayTableTexture(texture, view);
            }
        }

        /// <summary>
        /// Disposes GPU resources
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
        }
    }
}
