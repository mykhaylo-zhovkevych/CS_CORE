
namespace ObserverWeatherApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var CancelationTokenSource = new CancellationTokenSource();

            var channel = new BroadcastChannel();
            var pool = new UsersPool();

            var user1 = new User("Alice");
            var user2 = new User("Bob");


            pool.AddUser(user1, channel);
            pool.AddUser(user2, channel);
            var token = CancelationTokenSource.Token;

            await channel.SendMessageAsync(token);


        }
    }
}
