using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2._3._1.Warehouse;

namespace ConsoleApp2._3._1
{
    internal abstract class Order
    {

        public int OrderNumber { get; }
        public int Quantity { get; }
        public Product Product { get; }

        public abstract int Priority { get; }


        protected Order(int orderNumber, int quantity, Product product)
        {
            OrderNumber = orderNumber;
            Quantity = quantity;
            Product = product;
        }


        public virtual void ExecuteOn(AutomaticWagon wagon) => wagon.ProcessOrder(this);
    }

    internal class HighPriorityOrder : Order
    {
        public HighPriorityOrder(int orderNumber, int quantity, Product product) : base(orderNumber, quantity, product) { }

        public override void ExecuteOn(AutomaticWagon wagon)
        {
            Console.WriteLine("The highest priority");
            wagon.ProcessOrder(this);
        }

        public override int Priority => 1; 
    }

    internal class MiddlePriorityOrder : Order
    {
        public MiddlePriorityOrder(int orderNumber, int quantity, Product product) : base(orderNumber, quantity, product) { }

        public override int Priority => 2;
    }

    internal class LowPriorityOrder : Order
    {
        public LowPriorityOrder(int orderNumber, int quantity, Product product) : base(orderNumber, quantity, product) { }

        public override int Priority => 3;
    }
}