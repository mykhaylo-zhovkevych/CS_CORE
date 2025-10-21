using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public async Task PlacePrintIntervalOfOrdersAsync(uint each, List<Order> orders, CancellationToken ct )
        {
            while (!ct.IsCancellationRequested)
            {
                PlacePrintOrders(orders);
                await Task.Delay(TimeSpan.FromMilliseconds(each),ct);

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