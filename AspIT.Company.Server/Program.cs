using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using AspIT.Company.Common.Logging;
using AspIT.Company.Server.Config;
using AspIT.Company.Common.Entities;

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

            PrintServerInformation();

            server.ListenForTcpClients();
            // Subscribe to server events
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            bool closeServer = server == null;
            while(!closeServer)
            {
                // TODO: Refactor commands
                // Commands
                string input = Console.ReadLine();
                if(!ExecuteCommand(input))
                {
                    switch(input.ToLower())
                    {
                        case "close":
                            closeServer = true;
                            break;
                    }
                }
            }

            server.Shutdown();
        }

        private static void Server_ClientDisconnected(object sender, TcpClient client)
        {
            LogHelper.AddLog($"Client {client.Client.RemoteEndPoint} disconnected from server");
        }

        private static void Server_ClientConnected(object sender, TcpClient client)
        {
            LogHelper.AddLog($"Client connected from {client.Client.RemoteEndPoint}");
            server.ListenForTcpClients();
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="cmd">The command</param>
        /// <returns>A boolean that indicates if the command is valid, invalid or failed to execute</returns>
        private static bool ExecuteCommand(string cmd)
        {
            TcpClient client = server.ConnectedClients.ElementAt(0).Key;

            switch(cmd.ToLower())
            {
                case "force log":
                    LogHelper.AddLog("Log created.");
                    Log.Create();
                    return true;
                case "list clients":
                    LogHelper.AddLog("Connected clients:");
                    for(int i = 0; i < server.ConnectedClients.Count; i++)
                    {
                        client = server.ConnectedClients.ElementAt(i).Key;
                        LogHelper.AddLog(client.Client.RemoteEndPoint.ToString());
                    }
                    return true;
                case "getcl data":
                    User user = server.ConnectedClients[client][0] as User;
                    if(user != null)
                    {
                        Console.WriteLine($"{client.Client.RemoteEndPoint} Username: {user.Username}");
                    }
                    return true;
                case "disconnect all":
                    server.DisconnectAllClients();
                    return true;
                default:
                    Console.WriteLine("Invalid command");
                    return false;
            }
        }

        private static void PrintServerInformation()
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
