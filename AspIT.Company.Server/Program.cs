using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using AspIT.Company.Common.Logging;
using AspIT.Company.Server.Config;

namespace AspIT.Company.Server
{
    class Program
    {
        private static Server server;

        static void Main(string[] args)
        {
            LoadServerConfig();
            if(server == null)
            {
                LogHelper.AddLog("Server could not be loaded.", false);
                return;
            }

            PrintServerInformation(server);

            server.ListenForTcpClients();
            // Subscribe to server events
            server.ClientConnected += Server_ClientConnected;

            bool closeServer = server == null;
            while(!closeServer)
            {
                // TODO: Refactor commands
                // Commands
                switch(Console.ReadLine().ToLower())
                {
                    case "force log":
                        LogHelper.AddLog("Log created.");
                        Log.Create();
                        break;
                    case "list clients":
                        LogHelper.AddLog("Connected clients:");
                        for(int i = 0; i < server.ConnectedClients.Count; i++)
                        {
                            LogHelper.AddLog(server.ConnectedClients[i].Client.RemoteEndPoint.ToString());
                        }
                        break;
                    case "close":
                        closeServer = true;
                        break;
                }
            }

            server.Shutdown();
        }

        private static void Server_ClientConnected(object sender, TcpClient client)
        {
            LogHelper.AddLog($"Client connected from {client.Client.RemoteEndPoint}");
            server.ListenForTcpClients();
        }

        private static void PrintServerInformation(Server server)
        {
            Console.WriteLine($"Server name: {server.Name}");
            Console.WriteLine($"Server ip: {server.LocalEndpoint}");
            Console.WriteLine("Log:");
        }

        private static void LoadServerConfig()
        {
            ServerConfig config;

            if(!File.Exists("Configs/ServerConfig.xml"))
            {
                config = new ServerConfig($"Server {new Random().Next(0, 101)}", IPAddress.Parse("127.0.0.1"), 27013);
                config.Save();
            }

            try
            {
                config = ServerConfig.Load();
                server = new Server(config.ServerName, config.IPAddress, config.Port, true);
            }
            catch(Exception e) when(e.GetType() == typeof(SocketException))
            {
                LogHelper.AddLog(e.Message);
                server.Shutdown();
            }
        }
    }
}
