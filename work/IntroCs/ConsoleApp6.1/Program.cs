using ConsoleApp6._1.Menu;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp6._1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new Program().RunSimulationAsync();
        }

        public async Task RunSimulationAsync()
        {
            Restaurant restaurant = new Restaurant("Restaurant", "Main Station");

            var tasks = new List<Task>();

            for (int i = 0; i < 2; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        var order = restaurant.Counters[0].OrderFood(new List<IFoodItem> { new Burger("Cheeseburger", 5.99m) });
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }));
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        var order = restaurant.Counters[1].OrderFood(new List<IFoodItem> { new Coffe("Latte", 5.99m) });
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }));
            }

            await Task.WhenAll(tasks);

            var kitchenTasks = restaurant.Counters
                .Select(async counter =>
                {
                    try
                    {
                        await restaurant.Kitchen.PrepareOrderAsync(counter);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                })
                .ToList();

            await Task.WhenAll(kitchenTasks);
        }
    }
}