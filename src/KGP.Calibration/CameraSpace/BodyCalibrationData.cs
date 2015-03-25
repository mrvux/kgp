using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodyCalibrationFrame = System.Collections.Generic.IEnumerable<KGP.Calibration.CameraToCameraPoint>;


namespace KGP.Calibration
{
    /// <summary>
    /// Builds calibration data from kinect body
    /// </summary>
    public class BodyCalibrationData
    {
        private List<BodyCalibrationFrame> calibrationFrames;

        /// <summary>
        /// Constructor
        /// </summary>
        public BodyCalibrationData()
        {
            this.calibrationFrames = new List<BodyCalibrationFrame>();
        }

        /// <summary>
        /// Simple empty check
        /// </summary>
        public bool IsEmpty
        {
            get { return this.calibrationFrames.Count == 0; }
        }
            

        /// <summary>
        /// Returns full aggregated camera to camera point list
        /// </summary>
        public IList<CameraToCameraPoint> PointList
        {
            get
            {
                return calibrationFrames.SelectMany(cf => cf.ToList()).ToList();
            }     
        }

        /// <summary>
        /// Push a calibration frame
        /// </summary>
        /// <param name="frame">Frame calibration data</param>
        public void PushFrame(BodyCalibrationFrame frame)
        {
            this.calibrationFrames.Add(frame);
        }

        /// <summary>
        /// Remove last calibration frame
        /// </summary>
        public void RemoveLastFrame()
        {
            if (this.calibrationFrames.Count == 0)
                throw new Exception("calibration data is empty");

            this.calibrationFrames.RemoveAt(this.calibrationFrames.Count - 1);
        }
    }
}
