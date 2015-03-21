using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Contains a mapping between kinect camera space point and projected position
    /// </summary>
    public class CameraToScreenPoint
    {
        private readonly Vector3 cameraPoint;
        private readonly Vector2 screenPoint;

        /// <summary>
        /// Camera space point
        /// </summary>
        public Vector3 CameraPoint
        {
            get { return this.cameraPoint; }
        }

        /// <summary>
        /// Projected screen point
        /// </summary>
        public Vector2 ScreenPoint
        {
            get { return this.screenPoint; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cameraPoint">Camera point</param>
        /// <param name="screenPoint">Projected screen point</param>
        public CameraToScreenPoint(Vector3 cameraPoint, Vector2 screenPoint)
        {
            this.cameraPoint = cameraPoint;
            this.screenPoint = screenPoint;
        }
    }
}
