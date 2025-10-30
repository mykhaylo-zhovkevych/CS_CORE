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

            Console.WriteLine($"{KitchenName} has finished the process");
        }

        private async Task ProduceBigMacAsync()
        {
            Patty patty = await GrillPattyAsync();
            Bread top = await ToastBreadAsync();
            Bread bottom = await ToastBreadAsync();
            bottom = await CoatBreadWithSauceAsync();
            Bacon bacon = await GrillBaconAsync();

            var burger = new BigMac(top, bottom, bacon, patty);
            Console.WriteLine("Hamburger ready!");
        }

        private async Task ProduceCoffeeAsync()
        {
            Bread top = await ToastBreadAsync();
            var coffee = new Coffee(top);
            Console.WriteLine("Coffee ready!");
        }

        private async Task ProduceFriesAsync()
        {
            Sause sause= await PrepareSauseAsync();
            var coffee = new Fries(sause);
            Console.WriteLine("Friee ready!");
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
            await Task.Delay(20_000); // simuliert Grillzeit
            return new Patty() { IsGrilled = true };
        }

        private async Task<Bread> ToastBreadAsync()
        {
            Console.WriteLine("Toasting bread asynchronously...");
            await Task.Delay(5_000);
            return new Bread() { IsToasted = true };
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

        private async Task<Sause> PrepareSauseAsync()
        {
            Console.WriteLine("Preparing sauce asynchronously...");
            await Task.Delay(3_000);
            return new Sause() { IsCoveredOver = true };
        }

    }
}