using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Interface to provide camera to camera transformation 
    /// from two 3d point sets
    /// </summary>
    public interface ICameraToCameraSolver
    {
        /// <summary>
        /// Solves point set and gets a tranformation between those sets
        /// </summary>
        /// <param name="points">Point set to test</param>
        /// <returns>Affine transformation between each point set</returns>
        Matrix Solve(IEnumerable<CameraToCameraPoint> points);
    }
}
