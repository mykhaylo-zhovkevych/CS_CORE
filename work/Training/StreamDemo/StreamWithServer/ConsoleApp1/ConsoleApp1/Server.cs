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
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                while (client.Connected)
                {
                    string? message = await reader.ReadLineAsync();
                    if (message == null) break;

                    var parts = message.Split('|', 2);
                    if (parts.Length != 2) continue;

                    string mode = parts[0];
                    string text = parts[1];
                    string response = mode switch
                    {
                        "E" => Helper.Encrypt(text),
                        "D" => Helper.Decrypt(text),
                        _ => "Invalid mode"
                    };

                    await writer.WriteLineAsync(response);
                    Console.WriteLine($"Answer: {response}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}