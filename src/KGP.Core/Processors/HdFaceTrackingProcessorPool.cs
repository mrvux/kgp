using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Simple data structure to allow pooling face tracking processors
    /// </summary>
    public class HdFaceTrackingProcessorPool : IDisposable
    {
        private SingleHdFaceProcessor[] faceProcessors;
        private Dictionary<ulong, SingleHdFaceProcessor> activeProcessors;
        private List<SingleHdFaceProcessor> idleProcessors;

        /// <summary>
        /// Queries if a processor is available
        /// </summary>
        public bool IsAvailable
        {
            get { return this.idleProcessors.Count > 0; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="maxFaceCount">Maxmimum face count</param>
        public HdFaceTrackingProcessorPool(KinectSensor sensor, int maxFaceCount)
        {
            if (maxFaceCount < 1)
                throw new ArgumentOutOfRangeException("maxFaceCount", "Should be at least 1");

            this.faceProcessors = new SingleHdFaceProcessor[maxFaceCount];
            this.activeProcessors = new Dictionary<ulong, SingleHdFaceProcessor>();
            this.idleProcessors = new List<SingleHdFaceProcessor>(maxFaceCount);
            for (int i = 0; i < maxFaceCount; i++)
            {
                this.faceProcessors[i] = new SingleHdFaceProcessor(sensor);
                this.idleProcessors.Add(this.faceProcessors[i]);
            }
        }

        /// <summary>
        /// Acquires a face processor for a Kinect body
        /// </summary>
        /// <param name="body">Kinect body</param>
        /// <remarks>For now we only allow one processor per kinect body id</remarks>
        /// <returns>Face processor</returns>
        public SingleHdFaceProcessor Acquire(KinectBody body)
        {
            if (this.idleProcessors.Count == 0)
                throw new Exception("Pool is empty");
            if (this.activeProcessors.ContainsKey(body.TrackingId))
                throw new ArgumentException("body", "processor has already been allocated for this body tracking id");

            var processor = this.idleProcessors[this.idleProcessors.Count - 1];
            processor.AssignBody(body);

            this.idleProcessors.RemoveAt(this.idleProcessors.Count - 1);
            this.activeProcessors.Add(body.TrackingId, processor);
            return processor;
        }

        /// <summary>
        /// Releases a processor for a tracking id
        /// </summary>
        /// <param name="trackingId">tracking Id</param>
        public void Release(ulong trackingId)
        {
            if (this.activeProcessors.ContainsKey(trackingId))
            {
                var processor = this.activeProcessors[trackingId];
                processor.Suspend();

                this.activeProcessors.Remove(trackingId);
                this.idleProcessors.Add(processor);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < this.faceProcessors.Length; i++)
            {
                this.faceProcessors[i].Dispose();
            }
        }
    }
}
