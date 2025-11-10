using ConsoleApp6._1.Menu;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Counter
    {
        public string CounterName { get; private set; }
        public ConcurrentQueue<Order> PendingOrders { get; private set; } = new ConcurrentQueue<Order>();

        public Counter(string counterName)
        {
            CounterName = counterName;
        }

        // Reference from the MacDonalds ticket system 
        public Order OrderFood(List<IFoodItem> customerOrder)
        {
            if (customerOrder is null || customerOrder.Count == 0)
            {
                throw new ArgumentException("No food items selected. Please select at least one item to place an order");
            }

            List<GenericCustomer> buffer = new List<GenericCustomer>();
            Random random = new Random();
            var genericCustomer = new GenericCustomer(random.Next(100, 1000));

            // Temp in-memory buffer
            buffer.Add(genericCustomer);

            var order = new Order( genericCustomer.CustomerOrderNumber, customerOrder);

            PendingOrders.Enqueue(order);
            Console.WriteLine($"Your order is being processed, Order number is {order.CustomerOrderNumber}");
            return order;
        }
    }
}