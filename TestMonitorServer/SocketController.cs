using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestMonitorServer
{
    public class SocketController
    {
        /// <summary>
        /// This is  service control flag
        /// </summary>
        public bool RunFlag = true;

        /// <summary>
        /// Listens for connection from TCP network clients.
        /// </summary>
        public TcpListener Listener;

        /// <summary>
        /// This is the End Point for the communication
        /// </summary>
        public Socket MSocket;

        /// <summary>
        /// Stream to send Sockets to and from
        /// </summary>
        public Stream SocketStream;

        /// <summary>
        /// Network Stream reader
        /// </summary>
        private StreamReader Sreader;

        /// <summary>
        /// Network Stream writer
        /// </summary>
        private StreamWriter Swriter;

        /// <summary>
        /// Constructor for Monitor Server class
        /// </summary>
        /// <param name="localPort"></param>
        public SocketController(int localPort)
        {
            // Initialize a new instance of TCP listener class that listens
            // for incoming connection attempts on the Specified
            // Local IP Address and Port Number
            Listener = new TcpListener(IPAddress.Loopback, localPort);
            Listener.Start();

        }

        /// <summary>
        /// This Establishes Network Stream , Reads the Stream Data and Writes it to the console
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            int result = 0;

            MSocket = Listener.AcceptSocket();

            Console.WriteLine("Connected: " + MSocket.RemoteEndPoint);

            try
            {
                SocketStream = new NetworkStream(MSocket);
                Sreader = new StreamReader(SocketStream);
                Swriter = new StreamWriter(SocketStream);

                Swriter.AutoFlush = true;

                while (RunFlag)
                {
                    Console.WriteLine(Sreader.ReadLine());

                    Sreader.DiscardBufferedData();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Socket Error : " + ex.Message);
                result = -1;
            }

            return result;
        }

    }
}
