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
        private Order _order1;
        private Order _order2;
        private Order _order3;
        private PriorityQueue<Order, int> _orderQueue;


        [TestInitialize]
        public void Setup()
        {
            _orderQueue = new PriorityQueue<Order, int>();
            
            _order1 = new HighPriorityOrder(1, 2, new Food(1, "TestFood", 10));
            _order2 = new LowPriorityOrder(2, 5, new Material(2, "TestMaterial", 1));
            _order3 = new MiddlePriorityOrder(3, 3, new Material(3, "TestMaterial", 5));

        }

        // Check if orders adding into a queue
        [TestMethod]
        public void TestPreProcess_AddsOrderToQueue()
        {
            // Arrange

            // Act
            _orderQueue.Enqueue(_order1, _order1.Priority);
            _orderQueue.Enqueue(_order2, _order2.Priority);

            // Assert
            Assert.AreEqual(2, _orderQueue.Count);

        }

        // Check if orders correctly executing based on the priority 
        [TestMethod]
        public void TestExecuteOrder_SortesOrdersCorrectly()
        {
            // Arrange
            var currentOrder = _orderQueue;
            currentOrder.Enqueue(_order1, _order1.Priority);
            currentOrder.Enqueue(_order2, _order2.Priority);
            currentOrder.Enqueue(_order3, _order3.Priority);

            // Act

            var first = currentOrder.Dequeue();
            var secound = currentOrder.Dequeue();
            var third = currentOrder.Dequeue();


            // Assert

            Assert.AreEqual(0, currentOrder.Count);
            Assert.IsInstanceOfType(first, typeof(HighPriorityOrder));
            Assert.IsInstanceOfType(secound, typeof(MiddlePriorityOrder));
            Assert.IsInstanceOfType(third, typeof(LowPriorityOrder));

        }

        [TestMethod]
        public void TestProcessOrder_MoveOrderCorrect()
        {
            // Arrange
            var food = new Food(1, "Apple", 50);
            var source = new Cell(1, 100);

            source.StoreProduct(food);

            var wagon = new AutomaticWagon(101);
            wagon.AddCell(source);
            var order = new HighPriorityOrder(1001, 20, food);

            // Act
            wagon.ProcessOrder(order);

            // Assert
            Assert.IsTrue(source.HasEnoughProduct(order.Product, 30));
            // 100 - 30 = 70 >= 50 soll ja sein
            Assert.IsTrue(source.HasEnoughFreeSpace(50));
            
           
        }

        [TestMethod]
        public void TestProcessOrder_WithExceedingCellCapacity()
        {
            // Arrange
            var food = new Food(1, "Apple", 150);
            var source = new Cell(1, 100);   

            // Here I set wrong values directly 
            source.Products.Add(food);

            var wagon = new AutomaticWagon(101);
            wagon.AddCell(source);
            
            var order = new HighPriorityOrder(1001, 150, food);

            // Act / Assert
            Assert.ThrowsException<InvalidOperationException>(() => wagon.ProcessOrder(order));
            //Assert.IsTrue(ex.Message.Contains("No cell has enough of free space for Apple"));
        }
 


    }
}