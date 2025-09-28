using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2._3._1
{
    public abstract class Order
    {
        public int OrderNumber { get; }
        public int Quantity { get; }
        public Product Product { get; }
        public Cell SourceCell { get; }
        public Cell TargetCell { get; }
        public abstract int Priority { get; }

        protected Order(int orderNumber, int quantity, Product product, Cell sourceCell, Cell targetCell)
        {

            OrderNumber = orderNumber;
            Quantity = quantity;
            Product = product;
            SourceCell = sourceCell;
            TargetCell = targetCell;
        }

        public virtual void ExecuteOn(AutomaticWagon wagon) => wagon.ProcessOrder(this);
    }

    public class HighPriorityOrder : Order 
    { 
        public HighPriorityOrder(int n, int q, Product p, Cell s, Cell t) : base(n, q, p, s, t) { } 
        public override int Priority => 1; 
    }
    public class MiddlePriorityOrder : Order 
    { 
        public MiddlePriorityOrder(int n, int q, Product p, Cell s, Cell t) : base(n, q, p, s, t) { } 
        public override int Priority => 2; 
    }
    public class LowPriorityOrder : Order 
    { 
        public LowPriorityOrder(int n, int q, Product p, Cell s, Cell t) : base(n, q, p, s, t) {} 
        public override int Priority => 3; 
    }
}