using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Simple curve interface, to allow to ramp values
    /// </summary>
    public interface ICurve
    {
        /// <summary>
        /// Apply curve data
        /// </summary>
        /// <param name="value">Initial value</param>
        /// <remarks>Curve value is generally normalized from -1 to 1</remarks>
        /// <returns>Curved value</returns>
        float Apply(float value);
    }
}
