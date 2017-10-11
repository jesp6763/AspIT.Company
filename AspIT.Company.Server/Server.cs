using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AspIT.Company.Server
{
    public class Server
    {
        /// <summary>
        /// Gets or sets the display name of the server
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the TcpListener
        /// </summary>
        public TcpListener TcpListener { get; }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="tcpListener">A TcpListener</param>
        public Server(TcpListener tcpListener)
        {
            TcpListener = tcpListener;
        }

        /// <summary>
        /// Initializes a new instance of the Server class
        /// </summary>
        /// <param name="name">The name of the server. This is only for display</param>
        /// <param name="tcpListener">A TcpListener</param>
        public Server(string name, TcpListener tcpListener) : this(tcpListener)
        {
            Name = name;
        }
    }
}
