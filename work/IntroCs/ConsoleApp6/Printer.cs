using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Printer
    {
        //private readonly ConcurrentQueue<Order> _orderQueue = new ConcurrentQueue<Order>();
        //private readonly SemaphoreSlim _orderSignal = new SemaphoreSlim(0);
        //private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public ConcurrentQueue<Order> _orderQueue { get; set; } = new ConcurrentQueue<Order>();
        public SemaphoreSlim _orderSignal { get; set; }  = new SemaphoreSlim(0);
        public CancellationTokenSource _cts { get; set; } = new CancellationTokenSource();

        // Add another class that represent printer settings


        public async Task StartPrinter(CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                // It background waits/process order that is released, and blocks thread only no _orderSginal fired out
                // The WaitAsync decrements the _orderSignal count by 1
                // await _orderSignal.WaitAsync();

                while (HasOrdersInQueue())
                {
                    _orderQueue.TryDequeue(out var order);
                    Console.WriteLine($"Processing order {order.OrderName}...");
                    await ProcessOrderAsync(order);
                    Console.WriteLine($"Order {order.OrderName} completed.");
                }
                break;
            }
        }

        private bool HasOrdersInQueue() => !_orderQueue.IsEmpty;
       


        public void ExecuteOrder(List<Order> orders)
        {
            foreach (var order in orders)
            {
                _orderQueue.Enqueue(order);
                Console.WriteLine($"{order.OrderName} added to the queue.");
                //_orderSignal.Release();
            }

        }

        private async Task<bool> ProcessOrderAsync(Order order)
        {
            var duration = order.Size * 50;
            var sw = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                while (sw.ElapsedMilliseconds < duration)
                {
                    Console.WriteLine($"Printing {order.OrderName}: {sw.ElapsedMilliseconds * 100 / duration}% completed.");
                    await Task.Delay(25);
                }   
                sw.Stop();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing order {order.OrderId}: {ex.Message}");
            }
            return true;
        }

        public void StopPrinter() => _cts.Cancel();
    }
}