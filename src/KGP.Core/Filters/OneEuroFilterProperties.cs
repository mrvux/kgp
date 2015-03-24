using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    public class OneEuroFilterProperties
    {
        public OneEuroFilterProperties()
        {
            this.MinCutoff = 1.0f;
            this.Beta = 0.7f;
            this.DerivativeCutOff = 1.0f;
        }

        public float MinCutoff { get; set; }
        public float Beta { get; set; }
        public float DerivativeCutOff { get; set; }
    }
}
