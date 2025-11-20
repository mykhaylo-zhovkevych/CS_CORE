using Microsoft.VisualBasic;
using StreamDemo.StreamWithServer.ConsoleApp1.ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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

            TcpClient client = await listener.AcceptTcpClientAsync();
            await HandleClientAsync(client);
            
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using CaesarStream caesarStream = new CaesarStream(client.GetStream());

            while (client.Connected)
            {
                await caesarStream.WriteFromServerAsync(DateTime.Now.ToString("F"));
            }
            // client.Close();
        }
    }
}