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
            using TcpClient client = new TcpClient(_server, _port);
            using NetworkStream stream = client.GetStream();
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            while (true)
            {
                Console.Write("Mode [E]ncrypt / [D]ecrypt: ");
                string mode = Console.ReadLine().Trim().ToUpper();

                Console.Write("Text: ");
                string input = Console.ReadLine().Trim();

                // Send request to server
                await writer.WriteLineAsync($"{mode}|{input}");

                // Read response from server
                string response = await reader.ReadLineAsync();
                Console.WriteLine("Server response: " + response);

            }
        }
    }
}
