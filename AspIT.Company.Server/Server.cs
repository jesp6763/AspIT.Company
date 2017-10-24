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
        private List<TcpClient> connectedClients;

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
            connectedClients = new List<TcpClient>();
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        /// <param name="autoStart">Start server automatically</param>
        public Server(string name, IPEndPoint localEP, bool autoStart) : this(name, localEP)
        {
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
            connectedClients = new List<TcpClient>();
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        /// <param name="autoStart">Start server automatically</param>
        public Server(string name, IPAddress localaddr, int port, bool autoStart) : this(name, localaddr, port)
        {
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
        public List<TcpClient> ConnectedClients { get => connectedClients; }
        #endregion

        #region Methods
        public void ListenForTcpClients()
        {
            BeginAcceptTcpClient(ProcessClient, this);
        }

        private void ProcessClient(IAsyncResult asyncResult)
        {
            Server server = asyncResult.AsyncState as Server;
            TcpClient client = server.EndAcceptTcpClient(asyncResult);
            OnClientConnected(client);
            ConnectedClients.Add(client);
        }
        #endregion
    }
}
