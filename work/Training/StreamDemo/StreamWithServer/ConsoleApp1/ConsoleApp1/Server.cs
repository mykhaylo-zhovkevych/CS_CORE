using Microsoft.VisualBasic;
using StreamDemo.StreamWithServer.ConsoleApp1.ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Server
    {
        public static readonly int Port = 50005;

        public async Task Start()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, Port);
            listener.Start();

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int length = await stream.ReadAsync(buffer, 0, buffer.Length);

                await stream.WriteAsync(buffer, 0, length);
                await stream.FlushAsync();

                client.Close();
            }
        }
    }
}