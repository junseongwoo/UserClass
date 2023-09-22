using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserClass
{
    class NetworkHelper
    {
        #region [생성자]

        #endregion


        #region [Serial 통신]

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

        /// <summary>
        /// Serial Data 전송 메서드
        /// </summary>
        /// <param name="data"></param>
        public void SendData(SerialPort serial, string data)
        {
            try
            {
                if (serial.IsOpen)
                {
                    serial.WriteLine(data);
                }
                else
                {
                    MessageBox.Show("Serial Port is not Open");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serial Data 전송 에러 : " + ex.ToString());
            }
        }


        #endregion

        #region [TCP / IP 통신]

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
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public bool StartServer(TcpListener server, int port)
        {
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start Server Err : " + ex.ToString());
                return false;
            }
        }

    }
}
