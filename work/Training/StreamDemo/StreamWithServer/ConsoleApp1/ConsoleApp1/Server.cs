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
            // Waits for client connections
            TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, Port);
            listener.Start();

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                
                Task.Run(async () => await HandleClient(client));
            }

        }

        private async Task HandleClient(TcpClient client)
        {
            // Getting the data from TcpClient, because TcpClient dosent directly support reading and writing
            using (var writer = new StreamWriter(client.GetStream()))
            {
                while (client.Connected)
                {
                    string dateTime = DateTime.Now.ToString("F");
                    writer.WriteLine(dateTime);
                    writer.Flush();
                    await Task.Delay(1000);
                }
            }
            client.Close();
        }
    }
}