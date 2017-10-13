using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using AspIT.Company.Common.Logging;

namespace AspIT.Company.Server
{
    class Program
    {
        private static Server server;

        static void Main(string[] args)
        {
            server = new Server("Test Server", IPAddress.Parse("127.0.0.1"), 27013, true);
            PrintServerInformation(server);
            server.ListenForTcpClients();
            // Subscribe to server events
            server.ClientConnected += Server_ClientConnected;


            Console.ReadKey();
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
