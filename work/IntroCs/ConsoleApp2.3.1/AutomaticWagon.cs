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
    public class AutomaticWagon : IOrderExecutor<Order>
    {
        // Möglichkeit für mehere AutomaticWagons 
        private Order? _currentOrder;
        private Cell? _currentCell;
        private PriorityQueue<Order, int> _orderQueue = new PriorityQueue<Order, int>();
        public int WagonNumber { get; }
        
        public AutomaticWagon(int number) => WagonNumber = number;
        public void PreProcess(Order order)
        {
            _orderQueue.Enqueue(order, order.Priority);
            Console.WriteLine($"Added Order {order.OrderNumber} (Priority {order.Priority})");
        }

        public void ExecuteOrder()
        {
            while (_orderQueue.Count > 0)
            {
                _currentOrder = _orderQueue.Dequeue();
                Console.WriteLine($"Executing Order {_currentOrder.OrderNumber} (Priority {_currentOrder.Priority})");
                _currentOrder.ExecuteOn(this);
                //ProcessOrder( _currentOrder );
            }
        }

        public void ProcessOrder(Order order)
        {
            var sourceCell = _cells.FirstOrDefault(c => c.HasEnoughProduct(order.Product, order.Quantity));
            if (sourceCell == null)
                throw new InvalidOperationException($"No cell has enough of product {order.Product.Name} (Order {order.OrderNumber}).");

            MoveToCell(sourceCell);
            Load(order.Product, order.Quantity);

            var targetCell = _cells.FirstOrDefault(c => c.HasEnoughFreeSpace(order.Quantity));
            if (targetCell == null)
                throw new InvalidOperationException($"No cell has enough of free space for {order.Product.Name}.");
            Unload(order.Product, order.Quantity, targetCell);

            //Console.WriteLine($"Moving {order.Quantity} of {order.Product.Name} from Cell {sourceCell.Id} to Cell {targetCell.Id}");


        }

        private void Load(Product product)
        {
            _currentCell.RemoveProduct(product, qunatity);
        }

        private void Unload(Product product)
        {
            var type = product.GetType();
            var newProduct = Activator.CreateInstance(type, product.Id, product.Name, quantity);

            //cell.StoreProduct(newProduct);

        }

        private void MoveToCell(Cell cell)
        {
            if (!ReferenceEquals(_currentCell, cell))
            {
            _currentCell = cell;
            }
        }

        
    }
}