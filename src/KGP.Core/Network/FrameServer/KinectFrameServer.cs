using KGP.Serialization;
using KGP.Serialization.Images;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Network.FrameServer
{
    /// <summary>
    /// TCP Frame server for kinect
    /// <remarks>At the moment for quick test we only stream depth and body index</remarks>
    /// </summary>
    public class KinectFrameServer : IDisposable
    {
        private KinectSensor sensor;
        private readonly KinectClientListener listener;

        private TcpClient activeClient;
        private NetworkStream networkStream;

        private SnappyFrameCompressor depthCompressor;
        private SnappyFrameCompressor bodyIndexCompressor;

        private MultiSourceFrameReader multiSourceReader;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Server port</param>
        /// <param name="sensor"></param>
        public KinectFrameServer(int port, KinectSensor sensor)
        {
            this.sensor = sensor;
            this.listener = new KinectClientListener(port);
            this.listener.ClientConnected += listener_ClientConnected;

            this.depthCompressor = new SnappyFrameCompressor(KinectFrameInformation.DepthFrame);
            this.bodyIndexCompressor = new SnappyFrameCompressor(KinectFrameInformation.BodyIndexFrame);

            this.multiSourceReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.BodyIndex | FrameSourceTypes.Depth);
            this.multiSourceReader.MultiSourceFrameArrived += multiSourceReader_MultiSourceFrameArrived;
        }

        private void multiSourceReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (this.activeClient != null)
            {
                MultiSourceFrame frame = e.FrameReference.AcquireFrame();
                if (frame != null)
                {
                    this.ProcessDepth(frame);
                    this.ProcessBodyIndex(frame);

                    //Push depth frame
                    int depthPacketsize = this.depthCompressor.CompressedSize;

                    byte[] bheader = BitConverter.GetBytes(depthPacketsize);
                    this.networkStream.Write(bheader, 0, 4);
                    this.networkStream.Write(new byte[] { 0 }, 0, 1);

                    this.networkStream.Write(this.depthCompressor.CompressedFrameData, 0, depthPacketsize);

                    

                    //Push body index frame
                    int bodyIndexPacketsize = this.bodyIndexCompressor.CompressedSize;

                    bheader = BitConverter.GetBytes(bodyIndexPacketsize);
                    this.networkStream.Write(bheader, 0, 4);
                    this.networkStream.Write(new byte[] { 1 }, 0, 1);
                    this.networkStream.Write(this.bodyIndexCompressor.CompressedFrameData, 0, bodyIndexPacketsize);
                }
            }
        }

        private void ProcessDepth(MultiSourceFrame msf)
        {
            var depthFrame = msf.DepthFrameReference.AcquireFrame();
            if (depthFrame != null)
            {
                var buffer = depthFrame.LockImageBuffer();
                this.depthCompressor.Compress(buffer.UnderlyingBuffer);
                buffer.Dispose();

                depthFrame.Dispose();
            }
        }

        private void ProcessBodyIndex(MultiSourceFrame msf)
        {
            var bodyIndexFrame = msf.BodyIndexFrameReference.AcquireFrame();
            if (bodyIndexFrame != null)
            {
                var buffer = bodyIndexFrame.LockImageBuffer();
                this.bodyIndexCompressor.Compress(buffer.UnderlyingBuffer);
                buffer.Dispose();

                bodyIndexFrame.Dispose();
            }
        }

        private void listener_ClientConnected(object sender, KinectClientEventArgs e)
        {
            this.activeClient = e.Client;
            this.networkStream = e.NetworkStream;
        }

        /// <summary>
        /// Disposes the frame server
        /// </summary>
        public void Dispose()
        {
            this.listener.ClientConnected -= listener_ClientConnected;
            this.listener.Dispose();
        }
    }
}
