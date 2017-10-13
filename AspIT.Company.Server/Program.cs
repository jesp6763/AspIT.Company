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
                LogHelper.AddLog("Could not find config file Server.cfg", false);
                PrepareServerClose();
                return;
            }
            ReadServerConfig();
            PrintServerInformation(server);

            server.ListenForTcpClients();
            // Subscribe to server events
            server.ClientConnected += Server_ClientConnected;

            bool closeServer = false;
            while(!closeServer)
            {
                // TODO: Refactor commands
                // Commands
                switch(Console.ReadLine().ToLower())
                {
                    case "force log":
                        LogHelper.AddLog("Log created.", true);
                        Log.Create();
                        break;
                    case "close":
                        closeServer = true;
                        break;
                }
            }

            PrepareServerClose();
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

        private static void PrepareServerClose()
        {
            Log.AddLog(new Log.LogData("Server closed"));
            Log.Create(); // Generate log file
        }

        private static void ReadServerConfig()
        {
            // Read server.cfg
            using(StreamReader reader = File.OpenText("Server.cfg"))
            {
                string serverName = string.Empty;
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 27013;

                while(!reader.EndOfStream)
                {
                    string[] splitted = reader.ReadLine().Split('=');
                    string propertyName = splitted[0];
                    string propertyValue = splitted[1];

                    switch(propertyName)
                    {
                        case "ServerName":
                            serverName = propertyValue;
                            break;
                        case "IPAdress":
                            if(IPAddress.TryParse(propertyValue, out ipAddress))
                            {
                                LogHelper.AddLog($"Failed to read ip address. Using default ip address: {ipAddress.ToString()}");
                            }
                            break;
                        case "Port":
                            if(!int.TryParse(propertyValue, out port))
                            {
                                LogHelper.AddLog($"Failed to read port. Using default port: {port}");
                            }
                            break;
                    }
                }

                server = new Server(serverName, ipAddress, port, true);
            }
        }
    }
}
