using System.Net;
using System.Net.Sockets;

namespace ConsoleApp7
{
    public class TimeServer
    {
        static async Task Main(string[] args)
        {
            
            TcpListener listener = new TcpListener(IPAddress.Any, 5001);
            listener.Start();
            Console.WriteLine("Time server started...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                // starts a new task per client
                _ = Task.Run(async () =>
                {
                    using NetworkStream stream = client.GetStream();
                    // How do i set the stream to the writer?
                    using StreamWriter writer = new StreamWriter(stream);

                    while (client.Connected)
                    {
                        string time = DateTime.Now.ToString("HH:mm:ss");
                        await writer.WriteLineAsync(time);
                        Console.WriteLine($"Sent time: {time}");
                        await Task.Delay(1000); 
                    }
                });



            }
        }
    }
}
