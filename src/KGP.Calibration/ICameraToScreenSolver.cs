using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Interface used to perform camera to screen calibration
    /// </summary>
    public interface ICameraToScreenSolver
    {
        /// <summary>
        /// Solve camera to screen point set
        /// </summary>
        /// <param name="points">Point set</param>
        /// <returns>Camera to projector calibration result</returns>
        ProjectorCalibrationResult Solve(ProjectorCalibrationData data, IReadOnlyList<CameraToScreenPoint> points);
    }
}
