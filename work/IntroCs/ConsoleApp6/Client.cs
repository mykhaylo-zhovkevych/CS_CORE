using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Client
    {
        public string ClientName { get; internal set; }
        public Printer Printer { get; internal set; }
        // public List<Order> Orders { get; internal set; } = new List<Order>();

        public Client(string name, Printer printer)
        {
            ClientName = name;
            Printer = printer;
        }

        // Make generic method so bool with true can be returned, no exception is needed
        public async Task PlacePrintIntervalOfOrdersAsync(uint repitition, uint each, List<Order> orders, CancellationToken ct)
        {
            uint counter = 0;

            while (counter < repitition)
            {
                if (!ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();

                    PlacePrintOrders(orders);
                    await Task.Delay(TimeSpan.FromMilliseconds(each), ct);
                }
                counter++;
            }
        }

        public void PlacePrintOrders(List<Order> orders)
        {
            // Not really needed but for test possibility okay  
            if (Printer == null) throw new ArgumentNullException();
            Printer.ExecuteOrder(orders);
            
        }

        // Help methods
        // public void AddOrders(List<Order> orders) => Orders.AddRange(orders);
        
    }
}