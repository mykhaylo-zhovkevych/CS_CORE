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

        public async Task Run(string message)
        {
            using TcpClient client = new TcpClient(_server, _port);
            using CaesarStream caesarStream = new CaesarStream(client.GetStream()) { IsClient = true };


            // Send data to server
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await caesarStream.WriteAsync(data, 0, data.Length);
            await caesarStream.FlushAsync();

            // Read response from server
            byte[] buffer = new byte[1024];
            int length = await caesarStream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, length);

            Console.WriteLine("Response from Server: " + response);

            client.Close();
        }

    }
}
