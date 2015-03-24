using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace KGP
{
    public class OneEuroFilter
    {
        private readonly OneEuroFilterProperties properties;

        public OneEuroFilter(OneEuroFilterProperties properties)
        {
            firstTime = true;
            this.properties = properties;
            this.dxFilt = new LowpassFilter();
            this.xFilt = new LowpassFilter();
        }

        protected bool firstTime;
        protected LowpassFilter xFilt;
        protected LowpassFilter dxFilt;

        public float Filter(float x, float rate)
        {
            float dx = firstTime ? 0 : (x - xFilt.Last()) * rate;
            if (firstTime)
            {
                firstTime = false;
            }
            float edx = dxFilt.Filter(dx, Alpha(rate, properties.DerivativeCutOff));
            float cutoff = properties.MinCutoff + properties.Beta * Math.Abs(edx);

            return xFilt.Filter(x, Alpha(rate, cutoff));
        }

        protected float Alpha(float rate, float cutoff)
        {
            var tau = 1.0f / (2.0f * (float)Math.PI * cutoff);
            var te = 1.0f / rate;
            return 1.0f / (1.0f + tau / te);
        }
    }
}