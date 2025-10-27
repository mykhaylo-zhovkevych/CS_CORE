using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Kitchen
    {
        public string KitchenName { get; private set; }
        public Crew CurrentCrew { get; private set; }
        // Replace it with more genric class that holds burder as subtype of class itself


        public Kitchen(Crew currentCrew)
        {
            KitchenName = "Main Kitchen";
            CurrentCrew = currentCrew;

        }


        // Prepares Burgers asychron 
        public async Task PrepareOrderAsync(Counter counter)
        {

            if (counter.PendingOrders.IsEmpty)
            {
                throw new ArgumentException("No orders to process.");
            }

            while (counter.PendingOrders.TryDequeue(out var order))
            {
                await ProccessOrderAsync(order);
            }

        }

        private async Task ProccessOrderAsync(Order order)
        {
            Console.WriteLine("Has started the process");
            await CheckKitchenCapacity();
            Console.WriteLine("Has finished the process");

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

            var delay = presentMembers switch
            {
                6 => TimeSpan.FromMilliseconds(500),
                4 => TimeSpan.FromMilliseconds(1500),
                2 => TimeSpan.FromMilliseconds(2500),
                _ => TimeSpan.FromMilliseconds(3500)
            };
            return Task.Delay(delay);
        }

        private Task CheckOrderSize()
        {

            var orderedItems = 0;

            foreach (var item in )


        }


    }
}