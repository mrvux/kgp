using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace KGP.Direct3D11
{   
    public class TextureSize
    {
        private readonly Size2 value;

        public int Width
        {
            get { return value.Width; }
        }

        public int Height
        {
            get { return value.Height; }
        }

        public TextureSize(Size2 value)
        {
            Validate(value);
            this.value = value;
        }

        public TextureSize(int width, int height) : this(new Size2(width, height)) { }

        public static bool IsValid(Size2 candidate)
        {
            return (candidate.Width > 0 && candidate.Width <= 16384 && candidate.Height > 0 && candidate.Height <= 16384);
        }

        public static void Validate(Size2 candidate)
        {
            if (candidate.Width < 1 || candidate.Width > 16384)
                throw new ArgumentOutOfRangeException("width", "Width should be between 1 and 16384");
            if (candidate.Height < 1 || candidate.Height > 16384)
                throw new ArgumentOutOfRangeException("width", "Width should be between 1 and 16384");
        }


        public static implicit operator Size2(TextureSize value)
        {
            return value.value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

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
