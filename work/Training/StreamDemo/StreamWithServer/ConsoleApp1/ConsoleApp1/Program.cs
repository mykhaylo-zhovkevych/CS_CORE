using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Server server = new Server();
            Task serverTask = Task.Run(() => server.Start());

            Client client = new Client("localhost", Server.Port);
            client.Run().Wait();
        }
    }
}