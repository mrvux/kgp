using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Projector calibration data
    /// </summary>
    public class ProjectorCalibrationData
    {
        private readonly Vector2 sensorSize;
        private readonly float nearPlane;
        private readonly float farPlane;

        /// <summary>
        /// Sensor size, in pixels
        /// </summary>
        public Vector2 SensorSize
        {
            get { return this.sensorSize; }
        }

        /// <summary>
        /// Near plane
        /// </summary>
        public float Near
        {
            get { return this.nearPlane; }
        }

        /// <summary>
        /// Far plane
        /// </summary>
        public float Far
        {
            get { return this.farPlane; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensorSize">Sensor size</param>
        /// <param name="nearPlane">Near plane</param>
        /// <param name="farPlane">Far plane</param>
        public ProjectorCalibrationData(Vector2 sensorSize, float nearPlane, float farPlane)
        {
            this.sensorSize = sensorSize;
            this.nearPlane = nearPlane;
            this.farPlane = farPlane;
        }
    }
}
