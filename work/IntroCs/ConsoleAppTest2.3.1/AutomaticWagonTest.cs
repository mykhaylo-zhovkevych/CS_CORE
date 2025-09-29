using ConsoleApp2._3._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppTest2._3._1
{
    [TestClass]
    public class AutomaticWagonTest
    {

        private Cell cell1;
        private Cell cell2;

        private Food food;
        private Food food01;
        private Order _order;


        [TestInitialize]
        public void Setup()
        {
            cell1 = new Cell(1, 50);
            cell2 = new Cell(2, 50);


            food = new Food(1, "TestFood", 10);
      

            cell1.StoreProduct(food);

            _order = new HighPriorityOrder(101, 5, food, cell1, cell2);
         


        }


        // Checks if AutomaticWagon process orders correctly 
        [TestMethod]
        public void TestProcessOrder_MoveOrderCorrect()
        {
            // Arrange


            var wagon = new AutomaticWagon(101);


            // Act
            wagon.ProcessOrder(_order);

            // Assert
            Assert.AreEqual(5, cell2._products.Sum(p => p.ProductAmount));
            Assert.AreEqual(5, cell1._products.Sum(p => p.ProductAmount));
        }


        [TestMethod]
        public void TestPreProcess_AddsOrderToQueue()
        {
            // Arrange
            var cell1 = new Cell(1, 50);
            var cell2 = new Cell(2, 50);
            var cell3 = new Cell(3, 50);

            var food = new Food(1, "TestFood", 10);

            var orderQueue = new PriorityQueue<Order, int>();
            var order1 = new HighPriorityOrder(101, 5, food, cell1, cell2);
            var order2 = new LowPriorityOrder(202, 5, food, cell2, cell3);

            // Act
            orderQueue.Enqueue(order1, order1.Priority);
            orderQueue.Enqueue(order2, order2.Priority);

            // Assert
            Assert.AreEqual(2, orderQueue.Count);
        }

        // Check if orders correctly executing based on the priority 
        [TestMethod]
        public void TestExecuteOrder_SortesOrdersCorrectly()
        {
            // Arrange
            var cell1 = new Cell(1, 50);
            var cell2 = new Cell(2, 50);
            var cell3 = new Cell(3, 50);

            var food = new Food(1, "TestFood", 10);

            var order1 = new HighPriorityOrder(101, 5, food, cell1, cell2);
            var order2 = new LowPriorityOrder(202, 5, food, cell2, cell3);
            var order3 = new MiddlePriorityOrder(303, 1, food, cell3, cell1);

            var currentOrderQueue = new PriorityQueue<Order, int>();
            currentOrderQueue.Enqueue(order1, order1.Priority);
            currentOrderQueue.Enqueue(order2, order2.Priority);
            currentOrderQueue.Enqueue(order3, order3.Priority);

            // Act

            var first = currentOrderQueue.Dequeue();
            var secound = currentOrderQueue.Dequeue();
            var third = currentOrderQueue.Dequeue();


            // Assert

            Assert.AreEqual(0, currentOrderQueue.Count);
            Assert.IsInstanceOfType(first, typeof(HighPriorityOrder));
            Assert.IsInstanceOfType(secound, typeof(MiddlePriorityOrder));
            Assert.IsInstanceOfType(third, typeof(LowPriorityOrder));

        }

        

        [TestMethod]
        public void TestProcessOrder_WithExceedingCellCapacity()
        {
            // Arrange
            var cell1 = new Cell(1, 50);
            var cell2 = new Cell(2, 50);
   
            var food = new Food(1, "TestFood", 10);
            var wagon = new AutomaticWagon(101);
            cell1.StoreProduct(food);
            var order = new HighPriorityOrder(1001, 100, food, cell1, cell2);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => wagon.ProcessOrder(order));
        }
    }
}