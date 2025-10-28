namespace ConsoleApp6._1
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            Restaurant restaurant = new Restaurant("Restaurant", "Main Station");

            //restaurant.Counters.ForEach(counter =>
            //{
            //    Console.WriteLine($"Counter Name: {counter.CounterName}");
            //});


            var tasks = new List<Task>();

            for (int i = 0; i < 2; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    var order = restaurant.Counters[0].OrderFood();
              
                }));
                tasks.Add(Task.Run(() =>
                {
             
                    var order = restaurant.Counters[1].OrderFood();
                }));
            }

            await Task.WhenAll(tasks);


            var kitchenTasks = restaurant.Counters
                .Select(counter => restaurant.Kitchen.PrepareOrderAsync(counter))
                .ToList();

            await Task.WhenAll(kitchenTasks);

        }
    }
}