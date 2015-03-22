using FeralTic.DX11;
using FeralTic.DX11.Resources;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Network.FrameServer;
using KGP.Providers;
using KGP.Providers.Sensor;
using Microsoft.Kinect;
using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalStreamerSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            KinectFrameServer frameServer = new KinectFrameServer(35000, sensor);
            frameServer.ClientConnected += (sender, args) => Console.WriteLine("Client Connected");
            frameServer.ClientDisconnected += (sender, args) => Console.WriteLine("Client Disconnected");

            Console.ReadLine();

            sensor.Close();
        }

        static void frameServer_ClientConnected(object sender, EventArgs e)
        {
            
        }
    }
}
