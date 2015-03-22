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

        public ProjectorCalibrationResult(Matrix view, Matrix projection)
        {
            this.view = view;
            this.projection = projection;
        }
    }
}
