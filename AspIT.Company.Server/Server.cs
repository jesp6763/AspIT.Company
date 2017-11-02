using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using AspIT.Company.Common.Logging;
using AspIT.Company.Common.Entities;

namespace AspIT.Company.Server
{
    public class Server : TcpListener
    {
        /// <summary>
        /// Connected clients
        /// </summary>
        private Dictionary<TcpClient, List<object>> connectedClients;

        #region Events
        public event EventHandler<TcpClient> ClientConnected;
        public event EventHandler<TcpClient> ClientDisconnected;
        public event EventHandler<TcpClient> ClientSentData;
        private void OnClientConnected(TcpClient client)
        {
            ClientConnected?.Invoke(this, client);
        }
        private void OnClientDisconnected(TcpClient client)
        {
            ClientDisconnected?.Invoke(this, client);
        }
        private void OnClientSentData(TcpClient client)
        {
            ClientSentData?.Invoke(this, client);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        public Server(string name, IPEndPoint localEP) : base(localEP)
        {
            connectedClients = new Dictionary<TcpClient, List<object>>();
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
            connectedClients = new Dictionary<TcpClient, List<object>>();
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
        public string Name { get; }
        /// <summary>
        /// Gets all connected clients
        /// </summary>
        public Dictionary<TcpClient, List<object>> ConnectedClients => connectedClients;

        #endregion

        #region Methods
        /// <summary>
        /// Starts listening for incoming connections
        /// </summary>
        public void ListenForTcpClients()
        {
            BeginAcceptTcpClient(ProcessClient, null);
        }

        /// <summary>
        /// Processes a client to be accepted
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ProcessClient(IAsyncResult asyncResult)
        {
            TcpClient client = EndAcceptTcpClient(asyncResult);
            OnClientConnected(client);
            ConnectedClients.Add(client, new List<object>());
        }
        
        /// <summary>
        /// Shuts down the server
        /// </summary>
        public void Shutdown()
        {
            LogHelper.AddLog("Server closed", false);
            Log.Create();
        }

        /// <summary>
        /// Disconnects all clients from the server
        /// </summary>
        public void DisconnectAllClients()
        {
            foreach(KeyValuePair<TcpClient, List<object>> client in ConnectedClients)
            {
                DisconnectClient(client.Key);
            }

            ConnectedClients.Clear();
        }

        /// <summary>
        /// Disconnects a client
        /// </summary>
        public void DisconnectClient(TcpClient client)
        {
            client.Client.Disconnect(false);
            if (!client.Connected)
            {
                OnClientDisconnected(client);
                ConnectedClients.Remove(client);
                client.Dispose();
            }
        }
        #endregion
    }
}
