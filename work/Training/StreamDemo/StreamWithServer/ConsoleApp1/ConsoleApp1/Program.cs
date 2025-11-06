namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server();
            Task serverTask = Task.Run(() => server.Start());

            Client client = new Client("localhost", Server.Port);
            
            while (true)
            {
                Console.Write("Text send: ");
                string input = Console.ReadLine();

                client.Run(input).Wait();
            }
        }
    }
}
