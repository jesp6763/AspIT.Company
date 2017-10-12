using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AspIT.Company.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("Test Server", IPAddress.Parse("127.0.0.1"), 27013, true);
            PrintServerInformation(server);
            Console.ReadKey();
        }

        private static void PrintServerInformation(Server server)
        {
            Console.WriteLine($"Server name: {server.Name}");
            Console.WriteLine($"Server ip: {server.LocalEndpoint}");
            Console.WriteLine("Log:");
        }
    }
}
