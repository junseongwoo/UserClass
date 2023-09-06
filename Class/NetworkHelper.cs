using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserClass
{
    class NetworkHelper
    {
        /// <summary>
        /// Get Host IP 
        /// </summary>
        /// <returns>Host IP Address</returns>
        public string GetHostIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("1.1.1.1", 9001);

                IPEndPoint ipEndPoint = socket.LocalEndPoint as IPEndPoint;

                string localIPAddress = ipEndPoint.Address.ToString();

                return localIPAddress;
            }
        }

    }
}
