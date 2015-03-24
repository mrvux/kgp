using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Curves
{
    /// <summary>
    /// Simple linear curve, returns the same value as input
    /// </summary>
    public class LinearCurve : ICurve
    {
        /// <see cref="KGP.ICurve.Apply"/>
        public float Apply(float value)
        {
            return value;
        }
    }
}
