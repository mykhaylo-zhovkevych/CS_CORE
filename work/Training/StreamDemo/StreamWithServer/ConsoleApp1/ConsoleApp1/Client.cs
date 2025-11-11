using StreamDemo.StreamWithServer.ConsoleApp1.ConsoleApp1;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Client
    {
        private readonly string _server;
        private readonly int _port;

        public Client(string server, int port)
        {
            _server = server;
            _port = port;
        }

        public async Task Run()
        {
            TcpClient client = new TcpClient(_server, _port);
            using CaesarStream caesarStream = new CaesarStream(client.GetStream());

            while (client.Connected)
            {
                await caesarStream.ReadFromServerAsync();
            }
            // client.Close();
        }
    }
}
