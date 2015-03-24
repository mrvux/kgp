using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Curves
{
    /// <summary>
    /// Simple square curve
    /// </summary>
    public class SquareCurve : ICurve
    {
        /// <see cref="KGP.ICurve.Apply"/>
        public float Apply(float value)
        {
            return value * value * Math.Sign(value);
        }
    }
}
