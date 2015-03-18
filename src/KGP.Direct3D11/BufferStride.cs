using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11
{
    /// <summary>
    /// Buffer stride data type
    /// </summary>
    public class BufferStride
    {
        private readonly int value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Stride value</param>
        public BufferStride(int value)
        {
            Validate(value);
            this.value = value;
        }

        /// <summary>
        /// Tests if our buffer stride holds a valid value
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool IsValid(int candidate)
        {
            return candidate > 0 && (candidate % 4 == 0);
        }

        /// <summary>
        /// Validates stride value and throw appropriate exception if candidate is not valid
        /// </summary>
        /// <param name="candidate">Candidate to test</param>
        public void Validate(int candidate)
        {
            if (candidate < 1)
                throw new ArgumentOutOfRangeException("candidate", "Stride value should be greater than 0");

            if (candidate % 4 != 0)
                throw new ArgumentException("candidate", "Stride value should be a multiple of 4");
        }

        /// <summary>
        /// Converts back Buffer stride into int primitive type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator int(BufferStride value)
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
            var other = obj as BufferStride;
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
