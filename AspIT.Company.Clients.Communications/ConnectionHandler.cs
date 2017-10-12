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
        public static ConnectionResult TestServerConnection()
        {
            try
            {
                using (TcpClient client = new TcpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27013)))
                {
                    return ConnectionResult.ConnectionSuccess;
                }
            }
            catch (Exception e) when (e.GetType() == typeof(SocketException))
            {
                return ConnectionResult.ConnectionRefused;
                //return ConnectionResult.UnknownError;
            }
        }
    }
}
