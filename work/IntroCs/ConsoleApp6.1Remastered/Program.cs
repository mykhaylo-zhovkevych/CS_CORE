using ConsoleApp6._1.Menu;
using System;
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

            var list01 = new List<IFoodItem> { new BigMac("BigMac", 5.99m), new Coffee("Coffee", 12.9m) };
            // var list01 = new List<IFoodItem> { new BigMac("BigMac", 5.99m), new CheeseBurger("Coffee", 12.9m) };
            var list02 = new List<IFoodItem> { new Fries("Fries", 5.99m)};
            var list03 = new List<IFoodItem> { new BigMac("BigMac", 12.9m) };

            var tasks = new List<Task>();

            for (int i = 0; i < 1; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        var order = restaurant.Counters[0].OrderFood(list01);
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
                        var order01 = restaurant.Counters[1].OrderFood(list02);
                        var ordert02 = restaurant.Counters[1].OrderFood(list03);
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