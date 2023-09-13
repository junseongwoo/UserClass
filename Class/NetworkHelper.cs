using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserClass
{
    class NetworkHelper
    {
        #region [생성자]
        private SerialPort serialPort; 
        #endregion

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

        /// <summary>
        /// Serial Port 연결 
        /// </summary>
        /// <param name="serialPort"></param>
        /// <returns>Conntect = true</returns>
        public bool SerialConnect(SerialPort serialPort)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Serial Port 연결 끊기
        /// </summary>
        /// <param name="serialPort"></param>
        /// <returns>연결 끊기 성공 = true</returns>
        public bool SerialDisConnect(SerialPort serialPort)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
