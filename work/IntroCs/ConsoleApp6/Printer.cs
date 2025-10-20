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

        }


        public void ExecuteOrder(ConcurrentBag<Order> orders)
        {
            // The client sends order to be executed, how can I make them execute in order they were received?
            Task.Run(() =>
            {
                while (!_orderQueue.IsEmpty)
                {

                }
            }); 

        }

        public string PrintOrders()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();

        }
    }
}
