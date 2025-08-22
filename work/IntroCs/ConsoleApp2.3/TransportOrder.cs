using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3
{
    internal class TransportOrder
    {
        public TransportOrder(int tranportOrderNumber, int quantity, Cell sourceCell, Cell targetCell, Product product)
        {
            TranportOrderNumber = tranportOrderNumber;
            Quantity = quantity;
            SourceCell = sourceCell;
            TargetCell = targetCell;
            Product = product;
        }

        public int TranportOrderNumber { get; set; }
        public int Quantity { get; set; }
        public Cell SourceCell { get; set; }
        public Cell TargetCell { get; set; }
        public Product Product { get; set; }
    }
}

