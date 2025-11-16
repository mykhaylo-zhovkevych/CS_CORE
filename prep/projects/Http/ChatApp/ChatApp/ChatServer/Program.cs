using System.Net.Sockets;

namespace ChatServer
{
    public class Program
    {
        static List<Client> users;
        static TcpListener listener;
        public static void Main(string[] args)
        {
            users = new List<Client>();
            listener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 50001);
            listener.Start();

            while(true)
            {
                // Listener actually returns a TcpClient 
                // The Client is my own implementation
                var client = new Client(listener.AcceptTcpClient());
                users.Add(client);

                // Baordcast the connection to the evryone on the server

            }
        }
    }
}