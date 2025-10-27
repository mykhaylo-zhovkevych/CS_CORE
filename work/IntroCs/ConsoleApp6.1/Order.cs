using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Order
    {
        public Guid OrderId { get; private set; }
        public int CustomerOrderNumber { get; internal set; }
        public List<Burger> OrderAmount { get; internal set; }


        public Order(int customerOrderNumber, List<Burger> orderAmount)
        {
            OrderId = Guid.NewGuid();
            CustomerOrderNumber = customerOrderNumber;
            OrderAmount = orderAmount;
        }
    }
}