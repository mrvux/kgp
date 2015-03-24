using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    public class LowpassFilter
    {
        public LowpassFilter()
        {
            firstTime = true;
        }

        protected bool firstTime;
        protected float hatXPrev;

        public float Last()
        {
            return hatXPrev;
        }

        public float Filter(float x, float alpha)
        {
            float hatX = 0.0f;
            if (firstTime)
            {
                firstTime = false;
                hatX = x;
            }
            else
                hatX = alpha * x + (1.0f - alpha) * hatXPrev;

            hatXPrev = hatX;

            return hatX;
        }
    }
}
