using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2._3._1.Warehouse;

namespace ConsoleApp2._3._1
{
    internal class AutomaticWagon : IOrderExecutor<Order>
    {
        private Order? _currentOrder;
        private Cell? _currentCell;
        private PriorityQueue<Order, int> _orderQueue = new PriorityQueue<Order, int>();

        public int WagonNumber { get; }
        public List<Cell> Cells { get; set; } = new List<Cell>();

        // constructor
        public AutomaticWagon(int number) => WagonNumber = number;
        

        // Hilfsfunktion
        public void PreProcess(Order order)
        {
            _orderQueue.Enqueue(order, order.Priority);
            Console.WriteLine($"Added Order {order.OrderNumber} (Priority {order.Priority})");
        }


        public void ExecuteOrder(Order order)
        {
            
            if (_orderQueue.Count == 0)
            {
                Console.WriteLine("No orders to execute");
                return;
            }
            _currentOrder = _orderQueue.Dequeue();
            Console.WriteLine($"Executing Order {_currentOrder.OrderNumber} (Priority {_currentOrder.Priority})");
            _currentOrder.ExecuteOn(this);
            _currentOrder = null;
        }

        // needs a new Sorting Algorithm 
        public void ProcessOrder(Order order)
        {
          
            var sourceCell = Cells.FirstOrDefault(c => c.HasEnoughProduct(order.Product, order.Quantity));
            if (sourceCell == null)
                throw new InvalidOperationException($"No cell has enough of product {order.Product.Name} (Order {order.OrderNumber}).");

            MoveToCell(sourceCell);
            Load(order.Product, order.Quantity, sourceCell);

        }


        private void Load(Product product, int qunatity, Cell cell)
        {
            cell.RemoveProduct(product, qunatity);
        }

        private void Unload(Product product, int quantity, Cell cell)
        {
            cell.StoreProduct(new Product(product.ProductNumber, product.Name, quantity));
        }

        private void MoveToCell(Cell cell)
        {
            if (ReferenceEquals(_currentCell, cell)) return;
            _currentCell = cell;
        }

    }
}
