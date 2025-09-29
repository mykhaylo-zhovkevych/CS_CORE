using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2._3._1.Warehouse;

namespace ConsoleApp2._3._1
{
    public class AutomaticWagon
    {
        private readonly PriorityQueue<Order, int> _queue = new();
        public int WagonNumber { get; }
        public Cell? CurrentCell { get; private set; }

        // TODO: figure out the correct 
        public AutomaticWagon(int wagonNumber, Cell? startCell = null)
        {
            WagonNumber = wagonNumber;
            if (startCell != null)
            {
                CurrentCell = startCell;
            }
        }

        public void AddToOrderQueue(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            _queue.Enqueue(order, order.Priority);
        }

        public void ExecuteOrder()
        {
            while (_queue.Count > 0)
            {
                var order = _queue.Dequeue();
                ProcessOrder(order);
            }
        }

        private void ProcessOrder(Order order)
        {
            Console.WriteLine($"Processing Order {order.OrderNumber}: {order.Quantity}x " +
                $"{order.Product.Name} from Cell {order.SourceCell.Id} to Cell {order.TargetCell.Id}");

            MoveToCell(order.SourceCell);
            var loaded = Load(order.Product);
            MoveToCell(order.TargetCell);
            Unload(loaded, order.TargetCell);
        }

        private void MoveToCell(Cell cell)
        {
            if (!ReferenceEquals(CurrentCell, cell))
            {
                CurrentCell = cell;
            }
        }

        private Product Load(Product product)
        {
            if (CurrentCell == null)
                throw new InvalidOperationException($"Wagon {WagonNumber} Not located at any cell to load from");
            if (product == null) throw new ArgumentNullException(nameof(product));


            var removed = CurrentCell.RemoveProduct(product);

            Console.WriteLine($"Loaded {removed.ProductAmount} and {removed.Name} from Cell {CurrentCell.Id}");
            return removed;
        }

        private void Unload(Product product, Cell targetCell)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (targetCell == null) throw new ArgumentNullException(nameof(targetCell));

            targetCell.StoreProduct(product);
            Console.WriteLine($"Unloaded {product.ProductAmount}, {product.Name} into Cell {targetCell.Id}");
        }
    }
}