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
        public void StoreProduct_AddsProductToEmptyCell()
        {
            var cell = new Cell(1, 20);
            cell.StoreProduct(_food);

            Assert.AreEqual(1, cell.Products.Count);
            Assert.AreEqual(10, cell.Products.First().ProductAmount);
            Assert.AreEqual(101, cell.Products.First().Id);
        }



        [TestMethod]
        public void StoreProduct_MergesAmounts_WhenSameProductId()
        {
            var cell = new Cell(1, 50);
            cell.StoreProduct(_stone);

            cell.StoreProduct(new Material(_stone.Id, _stone.Name, 5));

            Assert.AreEqual(1, cell.Products.Count);
            Assert.AreEqual(15, cell.Products.First().ProductAmount);
        }


        [TestMethod]
        public void StoreProduct_ThrowsWhenNotEnoughSpace()
        {
            var cell = new Cell(1, 10);

       
            Assert.ThrowsException<InvalidOperationException>(() => cell.StoreProduct(_cloth));
        }

        [TestMethod]
        public void RemoveProduct_PartialRemovalReducesStoredAmount()
        {
            var cell = new Cell(1, 50);
            cell.StoreProduct(new Food(1, "Burger", 10));

            var removed = cell.RemoveProduct(new Food(1, "Burger", 5));

            Assert.AreEqual(5, removed.ProductAmount);
            Assert.AreEqual(5, cell.Products.First().ProductAmount);
        }

        [TestMethod]
        public void RemoveProduct_ThrowsWhenProductNotAvailable()
        {
            var cell = new Cell(1, 50);

            var request = new Food(999, "FoodTest", 1);
            Assert.ThrowsException<InvalidOperationException>(() => cell.RemoveProduct(request));
        }

    }
}
