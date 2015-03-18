using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11
{
    /// <summary>
    /// Simple class that represents buffer element count, to allow validation before to create Direct3d11 resource
    /// </summary>
    public class BufferElementCount
    {
        private readonly int elementCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elementCount">Element count</param>
        public BufferElementCount(int elementCount)
        {
            if (!IsValid(elementCount))
                throw new ArgumentOutOfRangeException("value", "Buffer element count should be at least 1");

            this.elementCount = elementCount;
        }

        /// <summary>
        /// Tests if our candidate value is a valid number for element count
        /// </summary>
        /// <param name="candidate">Candidate to test</param>
        /// <returns>ture if our candidate is a valid value as buffer element count, false otherwise</returns>
        public static bool IsValid(int candidate)
        {
            return candidate > 0;
        }

        /// <summary>
        /// Implicit int conversion, since buffer element count is a subset of int, 
        /// we can safely use implicit as no exception with be thrown
        /// </summary>
        /// <param name="value">Element count</param>
        /// <returns>Integer representation</returns>
        public static implicit operator int(BufferElementCount value)
        {
            return value.elementCount;
        }

        /// <summary>
        /// Converts our value into a string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return this.elementCount.ToString();
        }

        /// <summary>
        /// Equality test for element count
        /// </summary>
        /// <param name="obj">other object</param>
        /// <returns>true if both objects are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var other = obj as BufferElementCount;
            if (other == null)
                return base.Equals(obj);

            return object.Equals(this.elementCount, other.elementCount);
        }

        /// <see cref="System.Object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this.elementCount.GetHashCode();
        }
    }
}
