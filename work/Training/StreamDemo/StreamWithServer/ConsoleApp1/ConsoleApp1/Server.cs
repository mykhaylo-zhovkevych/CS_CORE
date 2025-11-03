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
                    string message = "Hello from server";
                    string encryptedMessage = SwapTheChars(message);

                    writer.WriteLine(encryptedMessage);

                    // Without Flush(), a writer may sit in the writer's buffer and not send data immediately, the Flush() forces the writer to send the data immediately 
                    writer.Flush();
                    await Task.Delay(1000);
                }
            }
            client.Close();
        }

        private string SwapTheChars(string lines)
        {
            if (string.IsNullOrEmpty(lines))
                return lines;

            int key = 3;

            var sb = new StringBuilder(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                char ch = lines[i];

                if (ch >= 'A' && ch <= 'Z')
                {
                    char enc = (char)(((ch + key - 65) % 26) + 65);
                    sb.Append(enc);
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    char enc = (char)(((ch + key - 97) % 26) + 97);
                    sb.Append(enc);
                }

            }

            return sb.ToString();
        }
    }
}