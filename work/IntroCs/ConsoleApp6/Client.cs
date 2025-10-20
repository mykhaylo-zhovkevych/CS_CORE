using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    // Client can set interval of the order execution or push order to be executed immediately
    public class Client
    {

        public string ClientName { get; internal set; }
        public Printer Printer { get; internal set; }
        public ConcurrentBag<Order> Orders { get; internal set; } = new ConcurrentBag<Order>();

        public Client(string name, Printer printer)
        {
            ClientName = name;
            Printer = printer;
        }

        public async Task PlacePrintIntervalOfOrdersAsnnc(uint each)
        {

            // 10 
            // 9 
            // 10 
            // get access from the ConcurrentBag<Order>

            // Each is in seconds
            while (!Orders.IsEmpty)
            {
                // Execute orders in the bag at the specified interval

                PlacePrintOrder();
                await Task.Delay(TimeSpan.FromSeconds(each));

            }
        }

        // dont wait for condition start immediatly
        public void PlacePrintOrder()
        {

            
            foreach (var order in Orders)
            {
                Printer.ExecuteOrder(order);
            }


            // clean the orders after or something another
        }




        // Help methods
        public void AddOrder(Order order) => Orders.Add(order);
    }
}
