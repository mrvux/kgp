using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Processes a single hd face
    /// </summary>
    public class SingleHdFaceProcessor : IDisposable
    {
        private HighDefinitionFaceFrameSource frameSource;
        private HighDefinitionFaceFrameReader framereader;
        private FaceModel faceModel = new FaceModel();
        private FaceAlignment faceAlignment = new FaceAlignment();
        private FaceModelBuilder faceModelBuilder;


        /// <summary>
        /// Raised when we got a new face model refreshed
        /// </summary>
        public event EventHandler<Tuple<FaceModel, FaceAlignment>> FaceModelRefreshed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        public SingleHdFaceProcessor(KinectSensor sensor)
        {
            this.frameSource = new HighDefinitionFaceFrameSource(sensor);
            this.framereader = this.frameSource.OpenReader();
            this.framereader.FrameArrived += this.FrameArrived;

            this.faceModelBuilder = this.frameSource.OpenModelBuilder(FaceModelBuilderAttributes.None);
            this.faceModelBuilder.CollectionCompleted += this.CollectionCompleted;
            this.faceModelBuilder.BeginFaceDataCollection();
        }

        private void CollectionCompleted(object sender, FaceModelBuilderCollectionCompletedEventArgs e)
        {
            var modelData = e.ModelData;
            this.faceModel = modelData.ProduceFaceModel();
            this.faceModelBuilder.Dispose();
            this.faceModelBuilder = null;
        }

        private void FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
        {
            using (HighDefinitionFaceFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (frame.IsTrackingIdValid == false) { return; }
                    frame.GetAndRefreshFaceAlignmentResult(this.faceAlignment);
                    //frame.Dispose();
                    if (this.FaceModelRefreshed != null)
                    {
                        this.FaceModelRefreshed(this, new Tuple<FaceModel, FaceAlignment>(this.faceModel, this.faceAlignment));
                    }
                }
            }
        }

        /// <summary>
        /// Assigns a kinect body for face tracking
        /// </summary>
        /// <param name="body">Body to assign</param>
        public void AssignBody(KinectBody body)
        {
            this.frameSource.TrackingId = body.TrackingId;
            this.framereader.IsPaused = false;
        }

        /// <summary>
        /// Pauses tracking
        /// </summary>
        public void Suspend()
        {
            this.framereader.IsPaused = true;
        }

        /// <summary>
        /// Check if tracking id is valid
        /// </summary>
        public bool IsValid
        {
            get { return this.frameSource.IsTrackingIdValid; }
        }

        /// <summary>
        /// Dispose class
        /// </summary>
        public void Dispose()
        {
            this.framereader.FrameArrived -= this.FrameArrived;
            this.framereader.Dispose();
        }
    }
}
