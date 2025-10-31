using System;
using System.Collections.Generic;
using System.Linq;
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
            using (TcpClient client = new TcpClient(_server, _port))

                using(StreamReader reader = new StreamReader(client.GetStream()))
                {
                while (!reader.EndOfStream)
                {
                    string? time = await reader.ReadLineAsync();
                    Console.WriteLine(time);
                }
            }

        }
    }
}
