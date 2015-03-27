using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Linear threshold curve, clamp while absolute value is within threshold
    /// </summary>
    public class ThresholdLinearCurve : ICurve
    {
        private float threshold;

        /// <summary>
        /// Current threshold
        /// </summary>
        public float Threshold
        {
            get { return this.threshold; }
            set { this.threshold = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="threshold">Initial threshold value</param>
        public ThresholdLinearCurve(float threshold)
        {
            this.threshold = threshold;
        }

        /// <summary>
        ///Apply threshold curve
        /// </summary>
        /// <param name="value">Intial value</param>
        /// <returns>Value with curve applied</returns>
        public float Apply(float value)
        {
            float absval = Math.Abs(value);
            if (absval <= threshold)
            {
                return 0.0f;
            }
            else
            {
                float diff = (absval - threshold) / (1.0f - threshold);
                return diff * Math.Sign(value);
            }
        }
    }
}
