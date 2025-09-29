using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2._3._1
{
    public abstract class Order
    {
        private readonly Product _product;

        public Product Product => _product;

        public int OrderNumber { get; }
        public Cell SourceCell { get; }
        public Cell TargetCell { get; }
        public abstract int Priority { get; }
        public int Quantity => _product.ProductAmount;
        

        protected Order(int orderNumber, Product product, Cell sourceCell, Cell targetCell)
        {

            OrderNumber = orderNumber;
            SourceCell = sourceCell;
            TargetCell = targetCell;
            if (product == null ) throw new ArgumentNullException(nameof(product));
            _product = product;

        }

    }

    public class HighPriorityOrder : Order 
    { 
        public HighPriorityOrder(int n, Product p, Cell s, Cell t) : base(n, p, s, t) { } 
        public override int Priority => 1; 
    }
    public class MiddlePriorityOrder : Order 
    { 
        public MiddlePriorityOrder(int n, Product p, Cell s, Cell t) : base(n, p, s, t) { } 
        public override int Priority => 2; 
    }
    public class LowPriorityOrder : Order 
    { 
        public LowPriorityOrder(int n, Product p, Cell s, Cell t) : base(n, p, s, t) {} 
        public override int Priority => 3; 
    }
}