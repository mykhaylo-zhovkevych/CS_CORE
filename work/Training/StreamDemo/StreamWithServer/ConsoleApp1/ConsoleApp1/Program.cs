namespace ConsoleApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Server server = new Server();
            Task serverTask = Task.Run(() => server.Start());

            var clients = new List<Task>();

            for (int i = 1; i <= 20; i++)
            {
                clients.Add(Task.Run(async () =>
                {
                    Client client = new Client("localhost", Server.Port);
                    await client.Run($"Hallo");
                }));
            }

            await Task.WhenAll(clients);
          

            //Client client = new Client("localhost", Server.Port);
            
            //while (true)
            //{
            //    Console.Write("Text send: ");
            //    string input = Console.ReadLine();

            //    client.Run(input).Wait();
            //}
        }
    }
}
