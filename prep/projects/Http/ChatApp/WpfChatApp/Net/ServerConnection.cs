using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfChatApp.Net
{
    public class ServerConnection
    {

        TcpClient clinet;

        public ServerConnection()
        {
            clinet = new TcpClient();
        }


        // Method that connects to the server
        public void ConnectToServer(string username)
        {
            if (!clinet.Connected)
            {
                clinet.Connect("127.0.0.1", 50001);
            }
        }


    }
}
