using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Printer
    {
        public Task? _backgroundTask;

        public ConcurrentQueue<Order> _orderQueue { get; set; } = new ConcurrentQueue<Order>();
        public CancellationTokenSource _cts { get; set; } = new CancellationTokenSource();

        // Add another class that represent printer settings

        public Task Start()
        {
            // Returns a pre-completed task that now holds reference to the running task
            // But the returned taks is immediatly returned even though the _backgroundTask is still running
            _backgroundTask = Task.Run(() => RunAsync(_cts.Token));
            return Task.CompletedTask;
        }

        private async Task RunAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_orderQueue.TryDequeue(out var order))
                {
                    Console.WriteLine($"Processing order {order.OrderName}...");
                    await ProcessOrderAsync(order);
                    Console.WriteLine($"Order {order.OrderName} completed.");
                }
                else
                {
                    await Task.Delay(500, token);
                }
            }
        }

        private async Task ProcessOrderAsync(Order order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            var duration = TimeSpan.FromSeconds(order.Size);
            var sw = System.Diagnostics.Stopwatch.StartNew();   

            while (sw.Elapsed.TotalSeconds < duration.TotalSeconds)
            {
                Console.WriteLine($"Printing {order.OrderName} of Size {order.Size}s\nPlease wait: {sw.Elapsed.TotalSeconds:F2}s");
                await Task.Delay(duration/10);
            }
            sw.Stop();
        }

        public void PreProcessOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                _orderQueue.Enqueue(order);
                Console.WriteLine($"{order.OrderName} added to the queue.");
            }
        }

        public void StopPrinter() => _cts.Cancel();
    }
}