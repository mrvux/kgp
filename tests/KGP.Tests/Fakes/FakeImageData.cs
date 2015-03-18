using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests.Fakes
{
    public unsafe static class FakeImageData
    {
        [DllImport("msvcrt.dll", SetLastError = false)]
        private static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);

        public static DepthFrameData RandomDepthFrame()
        {
            DepthFrameData data = new DepthFrameData();
            Random r = new Random();
            ushort[] d = new ushort[512 * 424];
            for (int i = 0; i < d.Length; i++)
            {
                d[i] = (ushort)r.Next(0, ushort.MaxValue);
            }

            fixed (ushort* ptr = &d[0])
            {
                memcpy(data.DataPointer, new IntPtr(ptr), 512 * 424 * 2);
            }

            return data;
        }

        public static BodyIndexFrameData RandomBodyIndexFrame()
        {
            BodyIndexFrameData data = new BodyIndexFrameData();
            Random r = new Random();
            byte[] d = new byte[512 * 424];
            for (int i = 0; i < d.Length; i++)
            {
                d[i] = (byte)r.Next(0, byte.MaxValue);
            }

            fixed (byte* ptr = &d[0])
            {
                memcpy(data.DataPointer, new IntPtr(ptr), 512 * 424);
            }
            return data;
        }

        public static bool ByteCheck(IntPtr p1, IntPtr p2, int length)
        {
            byte* b1 = (byte*)p1;
            byte* b2 = (byte*)p2;

            for (int i  = 0; i < length;i++)
            {
                if (b1[i] != b2[i])
                    return false;
            }

            return true;
        }
    }
}
