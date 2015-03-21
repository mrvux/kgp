using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Contains camera to projector calibration data
    /// </summary>
    public class CameraToProjectorData
    {
        private readonly Matrix view;
        private readonly Matrix projection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">View matrix</param>
        /// <param name="projection">Proejction matrix</param>
        public CameraToProjectorData(Matrix view, Matrix projection)
        {
            this.view = view;
            this.projection = projection;
        }
    }
}
