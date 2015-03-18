using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11
{
    /// <summary>
    /// Some extension methods for direct3d device
    /// </summary>
    public static class DeviceExtensions
    {
        [DllImport("msvcrt.dll", SetLastError = false)]
        private static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);

        /// <summary>
        /// Uploads texture content into gpu
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="context">Device context</param>
        /// <param name="dataPointer">Pointer to image data</param>
        /// <param name="size">Data size</param>
        /// <remarks>Since all kinect textures have a correct stride, we do not perform a copy per row, not do that check.</remarks>
        public static void Upload(this Texture2D texture, DeviceContext context, IntPtr dataPointer, int size)
        {
            DeviceContext ctx = context;
            DataStream ds;
            DataBox db = ctx.MapSubresource(texture, 0, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out ds);
            memcpy(db.DataPointer, dataPointer, size);
            context.UnmapSubresource(texture, 0);
        }

        /// <summary>
        /// Uploads buffer content into gpu
        /// </summary>
        /// <param name="buffer">Buffer to upload</param>
        /// <param name="context">Device context</param>
        /// <param name="dataPointer">Pointer to image data</param>
        /// <param name="size">Data size</param>
        /// <remarks>Since all kinect textures have a correct stride, we do not perform a copy per row, not do that check.</remarks>
        public static void Upload(this SharpDX.Direct3D11.Buffer buffer, DeviceContext context, IntPtr dataPointer, int size)
        {
            DeviceContext ctx = context;
            DataStream ds;
            DataBox db = ctx.MapSubresource(buffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out ds);
            memcpy(db.DataPointer, dataPointer, size);
            context.UnmapSubresource(buffer, 0);
        }
    }
}
