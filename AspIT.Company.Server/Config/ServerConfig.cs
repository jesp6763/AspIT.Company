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
    public class ServerConfig
    {
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
        public string ServerName { get; set; }
        /// <summary>
        /// Gets or sets the ip address
        /// </summary>
        public IPAddress IPAddress { get; set; }
        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Loads a server configuration
        /// </summary>
        /// <returns>A server configuration</returns>
        public static ServerConfig Load()
        {
            string path = "Configs/ServerConfig.xml";

            if(!File.Exists(path))
            {
                LogHelper.AddLog("Server config does not exist.");
                return null;
            }

            using(FileStream stream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
                return serializer.Deserialize(stream) as ServerConfig;
            }
        }

        /// <summary>
        /// Saves the server configuration
        /// </summary>
        /// <param name="path">Where to save the server configuration</param>
        public void Save()
        {
            string path = "Configs";

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using(FileStream stream = new FileStream(Path.Combine(path, "ServerConfig.xml"), FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
                serializer.Serialize(stream, this);
            }
        }
    }
}
