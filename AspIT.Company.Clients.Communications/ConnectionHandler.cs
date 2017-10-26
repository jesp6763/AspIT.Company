using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using AspIT.Company.Clients.Communications.Enums;

namespace AspIT.Company.Clients.Communications
{
    public static class ConnectionHandler
    {
        // TEST
        public static TcpClient CurrentConnection { get; set; }

        /// <summary>
        /// Connects a client to the server
        /// </summary>
        /// <returns>The result of the connection</returns>
        public static ConnectionResult ConnectToServer()
        {
            try
            {
                CurrentConnection = new TcpClient("127.0.0.1", 27013);
                return ConnectionResult.ConnectionSuccess;
            }
            catch (Exception e) when (e.GetType() == typeof(SocketException))
            {
                return ConnectionResult.ConnectionFailed;
                //return ConnectionResult.UnknownError;
            }
        }
    }
}