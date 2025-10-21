using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    // Can different orders accept and will sort them and execute them in order 
    public class Printer
    {
        private ConcurrentQueue<Order> _orderQueue = new ConcurrentQueue<Order>();

        public Printer()
        {
            // Start a background task to process orders

            Task.Run(async () =>
            {
                while (true)
                {
                    if (_orderQueue.TryDequeue(out Order order))
                    {
                        Console.WriteLine($"Processing order {order.OrderId} of size {order.Size}...");
                        await ProcessOrderSsync(order);
                        Console.WriteLine($"Order {order.OrderId} completed.");
                    }
                    else
                    {
                        // No orders to process, wait a bit before checking again
                        await Task.Delay(500);
                    }
                }
            });
        }


        public void ExecuteOrder(Order order)
        {
            // if 
            _orderQueue.Enqueue(order);
            Console.WriteLine("Order added to the queue.");

        }

        private async Task ProcessOrderSsync(Order order)
        {
            var duration = order.Size * 100;
            try
            {
                await Task.Delay(duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing order {order.OrderId}: {ex.Message}");
            }
        }

        public string PrintOrders()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();

        }
    }
}
