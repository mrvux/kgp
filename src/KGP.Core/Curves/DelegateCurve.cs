using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Curves
{
    /// <summary>
    /// Simple delegate curve
    /// </summary>
    public class DelegateCurve : ICurve
    {
        private readonly Func<float, float> curveFunction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="curveFunction">Curve function anonymous delegate</param>
        public DelegateCurve(Func<float,float> curveFunction)
        {
            if (curveFunction == null)
                throw new ArgumentNullException("curveFunction");

            this.curveFunction = curveFunction;
        }

        /// <see cref="KGP.ICurve.Apply"/>
        public float Apply(float value)
        {
            return curveFunction(value);
        }
    }
}
