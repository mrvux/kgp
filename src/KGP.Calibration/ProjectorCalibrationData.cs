using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    public class ProjectorCalibrationData
    {
        private readonly Vector2 sensorSize;
        private readonly float nearPlane;
        private readonly float farPlane;

        public Vector2 SensorSize
        {
            get { return this.sensorSize; }
        }

        public float Near
        {
            get { return this.nearPlane; }
        }

        public float Far
        {
            get { return this.farPlane; }
        }

        public ProjectorCalibrationData(Vector2 sensorSize, float nearPlane, float farPlane)
        {
            this.sensorSize = sensorSize;
            this.nearPlane = nearPlane;
            this.farPlane = farPlane;
        }
    }
}
