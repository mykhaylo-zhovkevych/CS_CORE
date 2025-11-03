using ConsoleApp6._1.Menu;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace ConsoleApp6._1
{
    public class Kitchen
    {
        public string KitchenName { get; private set; }
        public Crew CurrentCrew { get; private set; }

        public Kitchen(Crew currentCrew)
        {
            KitchenName = "Main Kitchen";
            CurrentCrew = currentCrew;
        }

        public async Task PrepareOrderAsync(Counter counter)
        {
            if (counter.PendingOrders.IsEmpty)
            {
                throw new ArgumentException("No orders to process");
            }

            while (counter.PendingOrders.TryDequeue(out var order))
            {
                Console.WriteLine($"Order from: {counter.CounterName}");
                await ProccessOrderAsync(order);
            }
        }

        private async Task ProccessOrderAsync(Order order)
        {
            Console.WriteLine($"Process started your, ID: {order.OrderId}");

            await CheckKitchenCapacity();
            if (order.OrderAmount.Any(item => item is BigMac))
            {
                await ProduceBigMacAsync();
            }

            if (order.OrderAmount.Any(item => item is Coffee))
            {
                await ProduceCoffeeAsync();
            }

            if (order.OrderAmount.Any(item => item is Fries))
            {
                await ProduceFriesAsync();
            }

            await CheckOrderSize(order.OrderAmount);

            Console.WriteLine($"{KitchenName} has finished the process");
        }

        private async Task ProduceBigMacAsync()
        {

            var pattyTask = GrillPattyAsync();
            var baconTask = GrillBaconAsync();

            var topBreadTask = ToastBreadAsync();

            var bottomBreadTask = CoatBreadWithSauceAsync();

            await Task.WhenAll(pattyTask, baconTask, topBreadTask, bottomBreadTask);

            // Repeat the parallel execution pattern with WhenAll
            var burger = new BigMac(topBreadTask.Result, bottomBreadTask.Result, baconTask.Result, pattyTask.Result);
            Console.WriteLine("Bigmac ready!");
        }

        private async Task ProduceCoffeeAsync()
        {
            Cookie bonus = await AddACookie();
            var coffee = new Coffee(bonus);
            Console.WriteLine("Coffee ready!");
        }

        private async Task ProduceFriesAsync()
        {
            Sauce sauce = await PrepareSauseAsync();
            var coffee = new Fries(sauce);
            Console.WriteLine("Frie ready!");
        }

        // The return type must be awaitable type
        private Task CheckKitchenCapacity()
        {
            var presentMembers = CurrentCrew.Members.Count;

            // Switch expression   
            var delay = presentMembers switch
            {
                >= 6 => TimeSpan.FromMilliseconds(500),
                >= 4 and < 6 => TimeSpan.FromMilliseconds(1500),
                >= 2 and < 4 => TimeSpan.FromMilliseconds(2500),
                _ => TimeSpan.FromMilliseconds(3500)
            };
            return Task.Delay(delay);
        }

        private Task CheckOrderSize(List<IFoodItem> orderSize)
        {
            var delay = orderSize.Count switch
            {
                >= 6 => TimeSpan.FromMilliseconds(3500),
                >= 4 and < 6 => TimeSpan.FromMilliseconds(2500),
                >= 2 and < 4 => TimeSpan.FromMilliseconds(1500),
                _ => TimeSpan.FromMilliseconds(500)
            };
            return Task.Delay(delay);
        }

        private async Task<Patty> GrillPattyAsync()
        {
            Console.WriteLine("Grilling patty asynchronously...");
            await Task.Delay(20_000);
            return new Patty() { IsGrilled = true };
        }

        private async Task<Bread> ToastBreadAsync()
        {
            Console.WriteLine("Toasting bread asynchronously...");
            await Task.Delay(5_000);
            return new Bread() { IsToasted = true };
        }

        private async Task<Cookie> AddACookie()
        {
            Console.WriteLine("Adding a Cookie...");
            await Task.Delay(5_000);
            return new Cookie() { IsServed = true };
        }

        private async Task<Bacon> GrillBaconAsync()
        {
            Console.WriteLine("Grilling bacon asynchronously...");
            await Task.Delay(10_000);
            return new Bacon() { IsGrilled = true };
        }

        private async Task<Bread> CoatBreadWithSauceAsync()
        {
            Console.WriteLine("Coating bread with sauce asynchronously...");
            await Task.Delay(2_000);
            return new Bread() { HasSauce = true };
        }

        private async Task<Sauce> PrepareSauseAsync()
        {
            Console.WriteLine("Preparing sauce asynchronously...");
            await Task.Delay(3_000);
            return new Sauce() { IsCoveredOver = true };
        }
    }
}