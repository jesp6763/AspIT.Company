﻿using System;
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

            // Buffer for reading data
            byte[] bytes = new byte[1024];
            string data;

            //Enter the listening loop
            while(true)
            {
                Console.WriteLine("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Client from address {client.Client.RemoteEndPoint} connected");

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                i = stream.Read(bytes, 0, bytes.Length);

                while(i != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(String.Format("Received: {0}", data));

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine(String.Format("Sent: {0}", data));

                    i = stream.Read(bytes, 0, bytes.Length);

                }

                // Shutdown and end connection
                client.Close();
            }
        }

        private static void PrintServerInformation(Server server)
        {
            Console.WriteLine($"Server name: {server.Name}");
            Console.WriteLine($"Server ip: {server.LocalEndpoint}");
            Console.WriteLine("Log:");
        }
    }
}
