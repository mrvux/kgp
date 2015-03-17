using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KinectFrameHeader
    {
        public uint Length;
        public uint Width;
        public uint Height;
    }
}
