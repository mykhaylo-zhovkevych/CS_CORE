using ConsoleApp2._3._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest2._3._1
{
    [TestClass]
    public class CellTest
    {

        private Product _food;
        private Product _stone;
        private Product _cloth;

        [TestInitialize] 
        public void Setup()
        {
            _food = new Food(101, "Burger", 10);
            _stone = new Material(202, "Stone", 10);
            _cloth = new Cloth(303, "Dress", 20);
        }

        [TestMethod]
        public void TestStoreProduct_AddsProductToEmptyCell()
        {
            // Arrange 
            var cell = new Cell(1, 20);

            // Act
            cell.StoreProduct(_food);

            // Assert
            Assert.AreEqual(1, cell._products.Count);
            Assert.AreEqual(10, cell._products.First().ProductAmount);

        }

        [TestMethod]
        public void TestStoreProduct_AddsToExistingProduct()
        {
            // Arrange
            var cell = new Cell(1, 50);
            cell.StoreProduct(_stone);

            // Act
            cell.StoreProduct(new Material(_stone.Id, _stone.Name, 5));

            // Assert
            Assert.AreEqual(1, cell._products.Count);
            Assert.AreEqual(15, cell._products.First().ProductAmount);
        }


        [TestMethod]
        public void TestStoreProduct_WhenNoSpaceAvailable()
        {
            // Arrange
            var cell = new Cell(1, 10);

            // Assert & Act
            Assert.ThrowsException<InvalidOperationException>(() => cell.StoreProduct(_cloth));
        }

        [TestMethod]
        public void TestRemoveProduct_OnlyPartially()
        {
            // Arrange
            var cell = new Cell(1, 50);
            cell.StoreProduct(new Food(1, "Burger", 10));

            // Act
            var removed = cell.RemoveProduct(new Food(1, "Burger", 5));

            // Assert
            Assert.AreEqual(5, removed.ProductAmount);
            Assert.AreEqual(5, cell._products.First().ProductAmount);
        }

        [TestMethod]
        public void TestRemoveProduct_WhenNoProductAvailable()
        {

            // Arrange
            var cell = new Cell(1, 50);

            // Assert & Act
            Assert.ThrowsException<NullReferenceException>(() => cell.RemoveProduct(null));

        }
    }
}
