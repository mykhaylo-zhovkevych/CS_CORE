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

        public async Task Run(string message)
        {
            using TcpClient client = new TcpClient(_server, _port);
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            // Send data to the Server
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await stream.WriteAsync(data);
            await stream.FlushAsync();

            string? line = await reader.ReadLineAsync();
            if (line != null)
            {
                Console.WriteLine("Received data: " + line);
                string decrypted = Helper.Decrypt(line);
                Console.WriteLine("Decrypted Data: " + decrypted);
            }

            client.Close();
        }
    }
}
