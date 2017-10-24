using System;
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
            BeginAcceptTcpClient(ProcessClient, this);
        }

        private void ProcessClient(IAsyncResult asyncResult)
        {
            Server server = asyncResult.AsyncState as Server;
            using(TcpClient client = server.EndAcceptTcpClient(asyncResult))
            {
                OnClientConnected(client);
                Log.AddLog(new Log.LogData($"Client connected from {client.Client.RemoteEndPoint}"));
            }
        }
        #endregion
    }
}
