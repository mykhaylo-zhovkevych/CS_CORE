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

            // Reads data from network stream
            using (StreamReader reader = new StreamReader(client.GetStream()))
            {
                string answer = await reader.ReadLineAsync();
                Console.WriteLine(answer);
            }
        }
    }
}
