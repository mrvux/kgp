using KGP.Direct3D11;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace KDP.Direct3D11.Tests
{
    [TestClass]
    public class TextureSizeTests
    {
        [TestMethod]
        public void ValidTest()
        {
            TextureSize textureSize = new TextureSize(16, 16);

            Size2 value = textureSize;
            Assert.AreEqual(value.Width, 16);
            Assert.AreEqual(value.Height, 16);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroWidthTest()
        {
            TextureSize textureSize = new TextureSize(0, 16);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroHeightTest()
        {
            TextureSize textureSize = new TextureSize(16, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroWidthSizeTest()
        {
            TextureSize textureSize = new TextureSize(new Size2(0, 16));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroHeightSizeTest()
        {
            TextureSize textureSize = new TextureSize(new Size2(16, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TooLargeWidthTest()
        {
            TextureSize textureSize = new TextureSize(new Size2(0, 16385));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TooLargeHeightTest()
        {
            TextureSize textureSize = new TextureSize(new Size2(16385, 0));
        }



    }
}
