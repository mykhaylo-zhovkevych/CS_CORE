using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3
{
    internal class ProcessOrders
    {
        private List<TransportOrder> orders = new List<TransportOrder>();

        public int ProcessingOrderNumber { get; set; }

        // Hilfsfunktionfür Objekt Verarbeitung 
        public void AddOrder(TransportOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }
            orders.Add(order);
        }
        public void ChangeOrderQuantity(TransportOrder order, int newQuantity )
        {
            int diff = newQuantity - order.Product.ProductAmount;

            // Mehr laden 
            if (diff > 0)
            {
                var additional = order.SourceCell.RemoveProduct(order.Product, diff);
                order.Product.ProductAmount += additional.ProductAmount;
            }

            // Weniger laden
            if (diff < 0)
            {
                var giveBack = new Product(order.Product.ProductNumber, order.Product.Name, -diff);
                order.SourceCell.StoreProduct(giveBack);
                order.Product.ProductAmount = newQuantity;
            }
            order.Quantity = newQuantity;

            Console.WriteLine($"Order {order.TranportOrderNumber}: Menge wurde auf {newQuantity} geändert.");
        }
    }
}
