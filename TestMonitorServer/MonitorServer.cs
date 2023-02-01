using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMonitorServer
{
    class MonitorServer
    {
        /// <summary>
        /// Port number for incoming socket
        /// </summary>
        public static int LocalPort = 56000;

        /// <summary>
        /// This is a Socket Handler for the Monitor Server
        /// </summary>
        public static SocketController Controller;

        /// <summary>
        /// Main Function where execution begins ,
        /// This can receive local port number 
        /// used for an application as an argument
        /// </summary>
        /// <param name="args"></param>
        static int Main(string[] args)
        {
            int result = 0;

            Console.WriteLine("Monitor Server Started");

            try
            {
                // process all command line arguments
                for(int i = 0; i<args.Length; i+=2)
                {
                    string nextArg = null;
                    if(args.Length > (i+1))
                    {
                        nextArg = args[i + 1];
                    }
                    switch( args[i])
                    {
                        // Command Line Argument -LP
                        case "-LP":
                        {
                                LocalPort = int.Parse(nextArg);
                                break;
                        }
                        default:
                        {
                                break;
                        }
                    }
                }

                Controller = new SocketController(LocalPort);

                Console.WriteLine( $"Server mounted on local host , port = {LocalPort }" );

                result = Controller.Start();
            }
            catch( Exception ex)
            {
                Console.WriteLine($"Server Error: {ex.Message}");
                Console.WriteLine("Press Enter to Continue");
                Console.ReadLine();

                result = -1;
            }

            try
            {
                Controller.Listener.Stop();
            }
            catch 
            {
                result = -1;
            }

            return (result = -1);

        }
    }
}
