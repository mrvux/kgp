using KGP.Frames;
using KGP.Providers;
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
    public class KinectFrameClient : IDepthFrameProvider, IBodyIndexFrameProvider
    {
        private TcpClient client;
        private IPEndPoint serverEndPoint;

        private NetworkStream clientStream;
        private Thread receiverThread;

        private DepthFrameData depthData = new DepthFrameData();
        private BodyIndexFrameData bodyIndexData = new BodyIndexFrameData();

        object objectLock = new Object();

        event EventHandler<DepthFrameDataEventArgs> depthReceived;
        event EventHandler<BodyIndexFrameDataEventArgs> bodyIndexReceived;


        event EventHandler<DepthFrameDataEventArgs> IDepthFrameProvider.FrameReceived
        {
            add
            {
                lock (objectLock)
                {
                    depthReceived += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    depthReceived -= value;
                }
            }
        }

        event EventHandler<BodyIndexFrameDataEventArgs> IBodyIndexFrameProvider.FrameReceived
        {
            add
            {
                lock (objectLock)
                {
                    bodyIndexReceived += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    bodyIndexReceived -= value;
                }
            }
        }


        private readonly byte[] temporaryData = new byte[KinectFrameInformation.DepthFrame.FrameDataSize];
        private byte[] header = new byte[5];
        private bool readHeader = true;
        private int bytesRead = 0;
        private int remaining = 0;
        private bool isDepth = false;

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
                    if (this.readHeader)
                    {
                        int p = this.clientStream.Read(this.header, 0, 5);
                        int packetLength = BitConverter.ToInt32(this.header, 0);
                        this.isDepth = this.header[4] == 0;

                        this.remaining = packetLength;
                        this.readHeader = false;
                        this.bytesRead = 0;
                    }

                    int totalRead = this.clientStream.Read(this.temporaryData, this.bytesRead, this.remaining);

                    this.bytesRead += totalRead;
                    this.remaining -= totalRead;

                    //Case where we have all the data we need
                    if (totalRead == this.remaining)
                    {
                        fixed (byte* bptr = &this.temporaryData[0])
                        {
                            if (isDepth)
                            {
                                SnappyFrameDecompressor.Uncompress(new IntPtr(bptr), bytesRead, this.depthData.DataPointer, this.depthData.SizeInBytes);

                                if (this.depthReceived != null)
                                {
                                    this.depthReceived(this, new DepthFrameDataEventArgs(this.depthData));
                                }
                            }
                            else
                            {
                                SnappyFrameDecompressor.Uncompress(new IntPtr(bptr), bytesRead, this.bodyIndexData.DataPointer, this.bodyIndexData.SizeInBytes);

                                if (this.bodyIndexReceived != null)
                                {
                                    this.bodyIndexReceived(this, new BodyIndexFrameDataEventArgs(this.bodyIndexData));
                                }
                            }
                        }

                        //Wait for next header
                        this.readHeader = true;
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
