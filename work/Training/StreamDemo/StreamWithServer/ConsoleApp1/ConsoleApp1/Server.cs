using Microsoft.VisualBasic;
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
                Task.Run(() => HandleClient(client));
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            try
            {
                // Receive data from Client
                string? clientMessage = await reader.ReadLineAsync();
                if (clientMessage == null) return;

                string encrypted = Helper.Encrypt(clientMessage);
                byte[] data = Encoding.UTF8.GetBytes(encrypted + "\n"); 

                await stream.WriteAsync(data);
                await stream.FlushAsync();
            }
            finally
            {
                client.Close();
            }

        } 
    }
}