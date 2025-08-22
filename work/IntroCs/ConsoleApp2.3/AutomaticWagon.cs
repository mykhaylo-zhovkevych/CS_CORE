using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3
{
    internal class AutomaticWagon
    {
        public int WagonNumber;

        private List<TransportOrder> activeOrders = new List<TransportOrder>();
        private Cell currentCell;
        private ProcessOrders manager = new ProcessOrders();

        public AutomaticWagon(int number)
        {
            WagonNumber = number;
        }

        public void ProcessOrders(TransportOrder order)
        {
            LoadOrder(order);


            Console.WriteLine($"Wagon {WagonNumber} verarbeitet Auftrag {order.TranportOrderNumber}");
            Console.WriteLine("Möchten Sie die Menge ändern? yes/no");

            string? input = Console.ReadLine();
            
            if (input?.ToLower() == "yes")
            {
                Console.Write("Bitte neune Menge eingeben: ");
                if (int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    manager.ChangeOrderQuantity(order, newQuantity);
                }
                else
                {
                    Console.WriteLine("Menge bleibt unverändert.");
                }
            }

            UnloadOrder(order);
        }

        public void LoadOrder(TransportOrder order)
        {
            if (currentCell != order.SourceCell)
            {
                MoveToCell(order.SourceCell);
            }
               

            Console.WriteLine($"Wagon {WagonNumber} lädt {order.Quantity}x {order.Product.Name} aus Zelle {order.SourceCell.CellNumber}");

            var removed = order.SourceCell.RemoveProduct(order.Product, order.Quantity);
            order.Product = removed;  // hier wird das Produkt aktualisiert, um die geladene Menge zu reflektieren
            activeOrders.Add(order);
        }

        public void UnloadOrder(TransportOrder order)
        {
            if (currentCell != order.TargetCell)
            {
                MoveToCell(order.TargetCell);
            }

            var activeOrder = activeOrders.FirstOrDefault(o => o.Product.ProductNumber == order.Product.ProductNumber);
            if (activeOrder == null)
                throw new InvalidOperationException("");

            Console.WriteLine($"Wagon {WagonNumber} entlädt {activeOrder.Quantity}x {activeOrder.Product.Name} in Zelle {activeOrder.TargetCell.CellNumber}");

            activeOrder.TargetCell.StoreProduct(activeOrder.Product);
            activeOrders.Remove(activeOrder);
        }

        public void MoveToCell(Cell cell)
        {
            currentCell = cell;
            Console.WriteLine($"Wagon {WagonNumber} bewegt sich zu Zelle {cell.CellNumber}");
        }
    }
}
