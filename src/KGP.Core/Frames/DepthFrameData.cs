using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Frames
{
    /// <summary>
    /// Depth frame data store, uses unmanaged memory back end
    /// </summary>
    public class DepthFrameData : IDisposable
    {
        private IntPtr dataPointer;

        /// <summary>
        /// Data pointer for depth frame data
        /// </summary>
        public IntPtr DataPointer
        {
            get 
            { 
                return this.dataPointer; 
            }
        }

        /// <summary>
        /// Constructor, allocates memory to hold depth frame
        /// </summary>
        public DepthFrameData()
        {
            this.dataPointer = Marshal.AllocHGlobal(Consts.DepthWidth * Consts.DepthHeight * sizeof(ushort));
        }

        /// <summary>
        /// Dispose memory for depth frame data
        /// </summary>
        public void Dispose()
        {
            if (this.dataPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.dataPointer);
                this.dataPointer = IntPtr.Zero;
            }   
        }
    }
}
