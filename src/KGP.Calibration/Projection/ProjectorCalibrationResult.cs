using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Projector calibration result
    /// </summary>
    public class ProjectorCalibrationResult
    {
        private readonly Matrix view;
        private readonly Matrix projection;

        /// <summary>
        /// View transform
        /// </summary>
        public Matrix View
        {
            get { return this.view; }
        }

        /// <summary>
        /// Projection transform
        /// </summary>
        public Matrix Projection
        {
            get { return this.projection; }
        }
            

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">View transform</param>
        /// <param name="projection">Projection transform</param>
        public ProjectorCalibrationResult(Matrix view, Matrix projection)
        {
            this.view = view;
            this.projection = projection;
        }
    }
}
