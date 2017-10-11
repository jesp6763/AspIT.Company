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
        public string Name { get; set; }
        public TcpListener TcpListener { get; }

        public Server(string name, TcpListener tcpListener)
        {
            Name = name;
            TcpListener = tcpListener;
        }
    }
}
