using Microsoft.Kinect;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    public class CameraToCameraPoint
    {
        private readonly Vector3 origin;
        private readonly Vector3 destination;

        public Vector3 Origin
        {
            get { return this.origin; }
        }

        public Vector3 Destination
        {
            get { return this.destination; }
        }

        public CameraToCameraPoint(Vector3 origin, Vector3 destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }
}
