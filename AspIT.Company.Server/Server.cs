using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AspIT.Company.Common.Logging;
using AspIT.Company.Common.Entities;

namespace AspIT.Company.Server
{
    public class Server : TcpListener
    {
        private Dictionary<TcpClient, List<object>> connectedClients;

        #region Events
        public event EventHandler<TcpClient> ClientConnected;
        public event EventHandler<TcpClient> ClientDisconnected;
        private void OnClientConnected(TcpClient client)
        {
            ClientConnected?.Invoke(this, client);
        }
        private void OnClientDisconnected(TcpClient client)
        {
            ClientDisconnected?.Invoke(this, client);
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
        public Dictionary<TcpClient, List<object>> ConnectedClients => connectedClients;

        #endregion

        #region Methods

        public void ListenForTcpClients()
        {
            BeginAcceptTcpClient(ProcessClient, this);
        }

        /// <summary>
        /// Processes a client to be accepted
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ProcessClient(IAsyncResult asyncResult)
        {
            Server server = asyncResult.AsyncState as Server;
            TcpClient client = server.EndAcceptTcpClient(asyncResult);
            OnClientConnected(client);
            ConnectedClients.Add(client, new List<object>());
            ConnectedClients[client].Add(GetClientData(client) as User);

            BeginLookingForData(client.Client);
        }

        /// <summary>
        /// Starts looking for incoming data that is sent from a client
        /// </summary>
        public void BeginLookingForData(Socket socket)
        {
            byte[] buffer = new byte[256];
            Server.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ProcessClientData, socket);
        }

        /// <summary>
        /// Processes a client's data
        /// </summary>
        private void ProcessClientData(IAsyncResult asyncResult)
        {

        }

        /// <summary>
        /// Retreives the client's sent object
        /// </summary>
        /// <param name="client">The client to retreive the object from</param>
        public object GetClientData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if(!stream.DataAvailable)
            {
                LogHelper.AddLog("Client has no data available.");
                return null;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            object sentObject = formatter.Deserialize(stream);
            return sentObject;
        }

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
