using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Simple gamma curve, applies a power function
    /// </summary>
    public class GammaCurve : ICurve
    {
        private readonly float power;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="power">Power exponent</param>
        public GammaCurve(float power)
        {
            if (power < 0.0f)
                throw new ArgumentOutOfRangeException("power", "Must be greater or equal than 0");

            this.power = power;
        }

        ///<see cref="KGP.ICurve.Apply"/>
        public float Apply(float value)
        {
            return (float)(Math.Pow(Math.Abs(value), this.power) * Math.Sign(value));
        }
    }
}
