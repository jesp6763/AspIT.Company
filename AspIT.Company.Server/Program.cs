using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using AspIT.Company.Common.Logging;

namespace AspIT.Company.Server
{
    class Program
    {
        private static Server server;

        static void Main(string[] args)
        {
            if(!File.Exists("Server.cfg"))
            {
                Log.AddLog(new Log.LogData("Could not find the config file Server.cfg"));
                Log.Create();
                return;
            }

            bool closeServer = false;

            // Read server.cfg
            using(StreamReader reader = File.OpenText("Server.cfg"))
            {
                reader.ReadLine();
            }
            server = new Server("Test Server", IPAddress.Parse("127.0.0.1"), 27013, true);
            PrintServerInformation(server);
            server.ListenForTcpClients();
            // Subscribe to server events
            server.ClientConnected += Server_ClientConnected;

            while(!closeServer)
            {
                // Commands
            }

            Log.AddLog(new Log.LogData("Server closed"));
            Log.Create(); // Generate log file
        }

        private static void Server_ClientConnected(object sender, TcpClient client)
        {
            Console.WriteLine($"Client connected from {client.Client.RemoteEndPoint}");
            server.ListenForTcpClients();
        }

        private static void PrintServerInformation(Server server)
        {
            Console.WriteLine($"Server name: {server.Name}");
            Console.WriteLine($"Server ip: {server.LocalEndpoint}");
            Console.WriteLine("Log:");
        }
    }
}
