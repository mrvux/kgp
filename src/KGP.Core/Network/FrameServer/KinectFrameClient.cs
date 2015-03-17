using KGP.Serialization.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KGP.Network.FrameServer
{
    /// <summary>
    /// Simple quick and dirty TCP Kinect frame client
    /// </summary>
    public class KinectFrameClient
    {
        private TcpClient client;
        private IPEndPoint serverEndPoint;

        private NetworkStream clientStream;
        private Thread receiverThread;

        private readonly SnappyFrameDecompressor depthDecompressor;
        private readonly SnappyFrameDecompressor bodyIndexDecompressor;

        /// <summary>
        /// (Ugly) Event when depth frame is received
        /// </summary>
        public event EventHandler<SnappyFrameDecompressor> DepthFrameReceived;

        /// <summary>
        /// (Ugly) Event when body Index frame is received
        /// </summary>
        public event EventHandler<SnappyFrameDecompressor> BodyIndexFrameArrived;

        private readonly byte[] temporaryData = new byte[KinectFrameInformation.DepthFrame.FrameDataSize];
        private byte[] header = new byte[5];

        private bool isRunning;

        /// <summary>
        /// Constructs a Kinect frame client
        /// </summary>
        /// <param name="ipAddress">Server address</param>
        /// <param name="port">Server port</param>
        public KinectFrameClient(IPAddress ipAddress, int port)
        {
            this.serverEndPoint = new IPEndPoint(ipAddress, port);
            this.client = new TcpClient();
            this.depthDecompressor = new SnappyFrameDecompressor(KinectFrameInformation.DepthFrame);
            this.bodyIndexDecompressor = new SnappyFrameDecompressor(KinectFrameInformation.BodyIndexFrame);
        }

        /// <summary>
        /// Connects to client
        /// </summary>
        public void Connect()
        {
            this.client.Connect(this.serverEndPoint);
            this.clientStream = client.GetStream();
            this.isRunning = true;
            this.receiverThread = new Thread(new ThreadStart(this.ReceiveThread));
            this.receiverThread.Start();
        }

        private unsafe void ReceiveThread()
        {
            while (this.isRunning)
            {
                if (this.clientStream.DataAvailable)
                {
                    int p = this.clientStream.Read(this.header, 0, 5);

                    int packetLength = BitConverter.ToInt32(this.header, 0);

                    bool isDepth = this.header[4] == 0;

                    int totalRead = this.clientStream.Read(this.temporaryData, 0, packetLength);

                    fixed (byte* bptr = &this.temporaryData[0])
                    {
                        if (isDepth)
                        {
                            this.depthDecompressor.UnCompress(new IntPtr(bptr), packetLength);
                            if (this.DepthFrameReceived != null)
                            {
                                this.DepthFrameReceived(this, this.depthDecompressor);
                            }
                        }
                        else
                        {
                            this.bodyIndexDecompressor.UnCompress(new IntPtr(bptr), packetLength);
                            if (this.BodyIndexFrameArrived != null)
                            {
                                this.BodyIndexFrameArrived(this, this.depthDecompressor);
                            }
                        }
                    }
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Stop receiving data
        /// </summary>
        public void Stop()
        {
            this.isRunning = false;
        }
     
    }
}
