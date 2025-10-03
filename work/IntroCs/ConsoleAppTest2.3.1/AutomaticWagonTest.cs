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

        [TestInitialize]
        public void Setup()
        {
            cell1 = new Cell(50, 50);
            cell2 = new Cell(50, 50);
        }


        [TestMethod]
        public void ExecuteOrder_MovesProdctFromSourceToTarget()
        {
            // Arrange
            var sourceProduct = new Food(1, "Burger", 10);
            cell1.StoreProduct(sourceProduct);

            var orderProduct = new Food(1, "Burger", 5);
            var order = new HighPriorityOrder(101, orderProduct, cell1, cell2);

            var wagon = new AutomaticWagon(101, cell1);

            // Act
            wagon.AddToOrderQueue(order);
            wagon.ExecuteOrder();

            // Assert
            Assert.AreEqual(5, cell2.Products[0].ProductAmount);
            //Assert.AreEqual(5, cell2.Products.Sum(p => p.ProductAmount));
            Assert.AreEqual(5, cell1.Products.Sum(p => p.ProductAmount));
            

            Assert.AreEqual(cell2, wagon.CurrentCell);
        }

        [TestMethod]
        public void ExecuteOrder_ThrowsWhenSourceDoesNotHaveEnoughProduct()
        {
            // Arrange
            var sourceProduct = new Food(1, "X", 10);
            cell1.StoreProduct(sourceProduct);

            var orderProduct = new Food(1, "X", 100);
            var order = new HighPriorityOrder(999, orderProduct, cell1, cell2);

            var wagon = new AutomaticWagon(301, cell1);
            wagon.AddToOrderQueue(order);

            // Act & Assert
            Assert.ThrowsExactly<NotEnoughProductException>(wagon.ExecuteOrder);
        }
    }
}