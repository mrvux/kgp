using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace KGP.Direct3D11
{   
    /// <summary>
    /// Texture size primitive type, encapsulates validation for size
    /// </summary>
    public class TextureSize
    {
        private readonly Size2 value;

        /// <summary>
        /// Width in pixels
        /// </summary>
        public int Width
        {
            get { return value.Width; }
        }

        /// <summary>
        /// Height in pixels
        /// </summary>
        public int Height
        {
            get { return value.Height; }
        }

        /// <summary>
        /// Constructor, using Size2
        /// </summary>
        /// <param name="value">Size</param>
        public TextureSize(Size2 value)
        {
            Validate(value);
            this.value = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        public TextureSize(int width, int height) : this(new Size2(width, height)) { }

        /// <summary>
        /// Do a validation test on a candidate
        /// </summary>
        /// <param name="candidate">Candidate value</param>
        /// <returns>true if candidate is valid, false otherwise</returns>
        public static bool IsValid(Size2 candidate)
        {
            return (candidate.Width > 0 && candidate.Width <= 16384 && candidate.Height > 0 && candidate.Height <= 16384);
        }

        /// <summary>
        /// Performs a validation test on a Size2 candidate, and throws appropriate exception if not valid
        /// </summary>
        /// <param name="candidate">Candidate to test</param>
        public static void Validate(Size2 candidate)
        {
            if (candidate.Width < 1 || candidate.Width > 16384)
                throw new ArgumentOutOfRangeException("width", "Width should be between 1 and 16384");
            if (candidate.Height < 1 || candidate.Height > 16384)
                throw new ArgumentOutOfRangeException("width", "Width should be between 1 and 16384");
        }

        /// <summary>
        /// Performs a validation test on a candidate, and throws appropriate exception if not valid
        /// </summary>
        /// <param name="height">Texture height</param>
        /// <param name="width">Texture width</param>
        public static void Validate(int width, int height)
        {
            Validate(new Size2(width, height));
        }

        /// <summary>
        /// Implicit converter from texture size to Size2
        /// </summary>
        /// <param name="value">Texture size</param>
        /// <returns>Size2 primitive equivalent</returns>
        public static implicit operator Size2(TextureSize value)
        {
            return value.value;
        }

        /// <see cref="System.Object.ToString"/>
        public override string ToString()
        {
            return this.value.ToString();
        }

        /// <see cref="System.Object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            var other = obj as TextureSize;
            if (other == null)
                return base.Equals(obj);

            return object.Equals(this.value, other.value);
        }

        /// <see cref="System.Object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

    }
}
