using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Curves
{
    public class ThresholdLinearCurve : ICurve
    {
        private float threshold;

        public float Threshold
        {
            get { return this.threshold; }
            set { this.threshold = value; }
        }

        public ThresholdLinearCurve(float threshold)
        {
            this.threshold = threshold;
        }

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
