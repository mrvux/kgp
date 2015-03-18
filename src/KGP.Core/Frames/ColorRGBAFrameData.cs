using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Frames
{
    /// <summary>
    /// Color frame data store (in rgba format)
    /// </summary>
    public class ColorRGBAFrameData : IDisposable
    {
        private IntPtr dataPointer;

        /// <summary>
        /// Data pointer for frame data
        /// </summary>
        public IntPtr DataPointer
        {
            get
            {
                if (this.dataPointer == IntPtr.Zero)
                    throw new ObjectDisposedException("ColorRGBAFrameData");

                return this.dataPointer;
            }
        }

        /// <summary>
        /// Constructor, allocates memory to hold frame data
        /// </summary>
        public ColorRGBAFrameData()
        {
            this.dataPointer = Marshal.AllocHGlobal(Consts.ColorWidth * Consts.ColorHeight * sizeof(int));
        }

        /// <summary>
        /// Dispose memory for frame data
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
