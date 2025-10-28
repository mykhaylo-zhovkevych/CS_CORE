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

        //public void UserInputTerminal()
        //{
        //    Console.WriteLine("--- Welcome To Restaurant ---");
        //    Console.WriteLine("1. Order");
        //    Console.WriteLine("2. Exit");
        //    var choice = PromptInt("Choose an option: ", 1, 2);
        //    switch (choice)
        //    {
        //        case 1: OrderFood(); break;
        //        case 2: Console.WriteLine("Canceled"); break;
        //    }
        //}


        // Reference from the MacDonalds ticket system 
        public Order OrderFood()
        {
            List<GenericCustomer> buffer = new List<GenericCustomer>();
            Random random = new Random();
            var genericCustomer = new GenericCustomer(random.Next(100, 1000));

            // Temp in-memory buffer
            buffer.Add(genericCustomer);

            var order = new Order(
               genericCustomer.CustomerOrderNumber,
               new List<IFoodItem>
               {
                    new Burger("Cheeseburger", 5.99m),
                    new Coffe("Late", 6.49m),
                    new Fries("Cheese Fries", 7.49m),
                    new Burger("Cheeseburger", 5.99m),
                    new Burger("Veggie Burger", 6.49m),
                    new Burger("Cheeseburger", 5.99m),
                    new Burger("Veggie Burger", 6.49m),
                    //new Burger("Cheeseburger", 5.99m),
                    //new Burger("Veggie Burger", 6.49m),
                    //new Burger("Cheeseburger", 5.99m),
                    //new Burger("Veggie Burger", 6.49m),
                    //new Burger("Cheeseburger", 5.99m),
                    //new Burger("Veggie Burger", 6.49m),
                    //new Burger("Cheeseburger", 5.99m),
                    //new Burger("Veggie Burger", 6.49m)
               });
       
            PendingOrders.Enqueue(order);
            Console.WriteLine($"Your order is being processed, Order number is {order.CustomerOrderNumber}.");
            return order;
        }

        //private int PromptInt(string message, int min = int.MinValue, int max = int.MaxValue)
        //{
        //    int value;
        //    do
        //    {
        //        Console.WriteLine(message);
        //    }
        //    while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
        //    return value;
        //}

    }
}