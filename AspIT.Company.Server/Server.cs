﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AspIT.Company.Common.Logging;

namespace AspIT.Company.Server
{
    public class Server : TcpListener
    {
        #region Events
        public event EventHandler<TcpClient> ClientConnected;
        protected virtual void OnClientConnected(TcpClient client)
        {
            ClientConnected?.Invoke(this, client);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        public Server(string name, IPEndPoint localEP) : base(localEP)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        /// <param name="autoStart">Start server automatically</param>
        public Server(string name, IPEndPoint localEP, bool autoStart) : base(localEP)
        {
            Name = name;

            if(autoStart)
            {
                Start();
            }
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        public Server(string name, IPAddress localaddr, int port) : base(localaddr, port)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        /// <param name="autoStart">Start server automatically</param>
        public Server(string name, IPAddress localaddr, int port, bool autoStart) : base(localaddr, port)
        {
            Name = name;

            if(autoStart)
            {
                Start();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the display name of the server
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Methods
        public void ListenForTcpClients()
        {
            BeginAcceptTcpClient(WaitForConnectingUser, this);
        }

        private void WaitForConnectingUser(IAsyncResult asyncResult)
        {
            Server server = asyncResult.AsyncState as Server;
            using(TcpClient client = server.EndAcceptTcpClient(asyncResult))
            {
                OnClientConnected(client);
                Log.AddLog(new Log.LogData($"Client connected from {client.Client.LocalEndPoint}"));
            }

            /*
            // Buffer for reading data
            byte[] bytes = new byte[1024];
            string data;

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
            client.Close();*/
        }
        #endregion
    }
}
