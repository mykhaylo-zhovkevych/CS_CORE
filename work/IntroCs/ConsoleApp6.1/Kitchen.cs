using ConsoleApp6._1.Menu;
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
                throw new ArgumentException("No orders to process.");
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
            await CheckOrderSize(order.OrderAmount);
            Console.WriteLine($"{KitchenName} has finished the process");

        }

        // The return type must be awaitable type
        private Task CheckKitchenCapacity()
        {
            var presentMembers = 0;

            foreach (var member in CurrentCrew.members)
            {
                if (member is not null)
                {
                    presentMembers++;
                }
            }

            // Switch expression   
            var delay = presentMembers switch
            {
                6 => TimeSpan.FromMilliseconds(500),
                4 => TimeSpan.FromMilliseconds(1500),
                2 => TimeSpan.FromMilliseconds(2500),
                _ => TimeSpan.FromMilliseconds(3500)
            };
            return Task.Delay(delay);
        }

        private Task CheckOrderSize(List<IFoodItem> orderSize)
        {
            var counter = 0;

            foreach (var item in orderSize)
            {
                if (item is not null)
                {
                    counter++;
                }
            }

            var delay = counter switch
            {
                6 or > 6 => TimeSpan.FromMilliseconds(3500),
                4 => TimeSpan.FromMilliseconds(2500),
                2 => TimeSpan.FromMilliseconds(1500),
                _ => TimeSpan.FromMilliseconds(500)
            };
            return Task.Delay(delay);
        }
    }
}