using Microsoft.Kinect;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Camera to camera point tuple
    /// </summary>
    public class CameraToCameraPoint
    {
        private readonly Vector3 origin;
        private readonly Vector3 destination;

        /// <summary>
        /// Origin position
        /// </summary>
        public Vector3 Origin
        {
            get { return this.origin; }
        }

        /// <summary>
        /// Destination position
        /// </summary>
        public Vector3 Destination
        {
            get { return this.destination; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin">Origin position</param>
        /// <param name="destination">Destination position</param>
        public CameraToCameraPoint(Vector3 origin, Vector3 destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }
}
