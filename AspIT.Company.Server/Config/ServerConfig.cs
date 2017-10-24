using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;

namespace AspIT.Company.Server.Config
{
    /// <summary>
    /// Represents a server configuration
    /// </summary>
    public class ServerConfig : Config<ServerConfig>
    {
        private string serverName;
        private IPAddress ipAddress;
        private int port;

        /// <summary>
        /// Initializes a new instance of the ServerConfig class with default settings
        /// </summary>
        public ServerConfig()
        {
            ServerName = $"Server {new Random().Next(0, 101)}";
            IPAddress = IPAddress.Parse("127.0.0.1");
            Port = 27013;
        }

        /// <summary>
        /// Initializes a new instance of the ServerConfig class
        /// </summary>
        /// <param name="serverName">The server name</param>
        /// <param name="ipAddress">The server ip address</param>
        /// <param name="port">The server port</param>
        public ServerConfig(string serverName, IPAddress ipAddress, int port)
        {
            ServerName = serverName;
            IPAddress = ipAddress;
            Port = port;
        }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        public string ServerName { get => serverName; set => serverName = value; }
        /// <summary>
        /// Gets or sets the ip address
        /// </summary>
        [XmlIgnore]
        public IPAddress IPAddress { get => ipAddress; set => ipAddress = value; }
        [XmlElement("IPAddress")]
        public string IPAddressForXML
        {
            get { return IPAddress.ToString(); }
            set
            {
                IPAddress = string.IsNullOrEmpty(value) ? null : IPAddress.Parse(value);
            }
        }
        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get => port; set => port = value; }
    }
}
