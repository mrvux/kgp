using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Tracks several kinect faces
    /// </summary>
    public class MultipleFaceProcessor : IDisposable
    {
        private readonly BodyTrackingProcessor bodyProcessor;
        private SingleFaceProcessor[] faceProcessors;
        private Dictionary<ulong, SingleFaceProcessor> activeProcessors;
        private List<SingleFaceProcessor> idleProcessors;

        private Dictionary<ulong, FaceFrameResultEventArgs> currentResults;

        /// <summary>
        /// Number of active face trackers
        /// </summary>
        public int ActiveProcessorsCount
        {
            get { return this.activeProcessors.Count; }
        }

        /// <summary>
        /// Current active face tracking results
        /// </summary>
        public List<FaceFrameResultEventArgs> CurrentResults
        {
            get { return this.currentResults.Values.ToList(); }
        }

        /// <summary>
        /// Raised when frame results changed
        /// </summary>
        public event EventHandler OnFrameResultsChanged;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="bodyProcessor">Kinect body processor</param>
        /// <param name="maxFaceCount">Maximum face count</param>
        public MultipleFaceProcessor(KinectSensor sensor, BodyTrackingProcessor bodyProcessor, int maxFaceCount)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");
            if (bodyProcessor == null)
                throw new ArgumentNullException("bodyProcessor");
            if (maxFaceCount < 1)
                throw new ArgumentOutOfRangeException("maxFaceCount", "Should be at least 1");

            this.bodyProcessor = bodyProcessor;
            this.bodyProcessor.BodyTrackingStarted += BodyTrackingStarted;
            this.bodyProcessor.BodyTrackingLost += BodyTrackingLost;

            this.faceProcessors = new SingleFaceProcessor[maxFaceCount];
            this.activeProcessors = new Dictionary<ulong, SingleFaceProcessor>();
            this.idleProcessors = new List<SingleFaceProcessor>(maxFaceCount);
            this.currentResults = new Dictionary<ulong, FaceFrameResultEventArgs>();
            for (int i = 0; i < maxFaceCount; i++)
            {
                this.faceProcessors[i] = new SingleFaceProcessor(sensor);
                this.idleProcessors.Add(this.faceProcessors[i]);
            }
        }

        private void RaiseTrackingResultsChanged()
        {
            if (this.OnFrameResultsChanged != null)
            {
                this.OnFrameResultsChanged(this, new EventArgs());
            }
        }

        private void BodyTrackingStarted(object sender, KinectBodyEventArgs e)
        {
            if (this.idleProcessors.Count > 0)
            {
                var processor = this.idleProcessors[this.idleProcessors.Count - 1];
                processor.FaceResultAcquired += FaceResultAcquired;
                processor.AssignBody(e.Body);


                this.idleProcessors.RemoveAt(this.idleProcessors.Count - 1);
                this.activeProcessors.Add(e.Body.TrackingId, processor);
            }
        }

        private void BodyTrackingLost(object sender, KinectBodyEventArgs e)
        {
            if (this.activeProcessors.ContainsKey(e.Body.TrackingId))
            {
                var processor = this.activeProcessors[e.Body.TrackingId];
                processor.FaceResultAcquired -= FaceResultAcquired;
                processor.Suspend();

                this.activeProcessors.Remove(e.Body.TrackingId);
                this.idleProcessors.Add(processor);

                //Remove from tracked list if relevant
                if (this.currentResults.ContainsKey(e.Body.TrackingId))
                {
                    this.currentResults.Remove(e.Body.TrackingId);
                    this.RaiseTrackingResultsChanged();
                }
            }
        }

        private void FaceResultAcquired(object sender, FaceFrameResultEventArgs e)
        {
            this.currentResults[e.TrackingId] = e;
            this.RaiseTrackingResultsChanged();
        }

        /// <summary>
        /// Disposes resources and clean event wiring
        /// </summary>
        public void Dispose()
        {
            this.bodyProcessor.BodyTrackingStarted -= BodyTrackingStarted;
            this.bodyProcessor.BodyTrackingLost -= BodyTrackingLost;

            for (int i = 0; i < this.faceProcessors.Length; i++)
            {
                this.faceProcessors[i].Dispose();
            }
        }
    }
}
