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
            var cell = new Cell(20, 50);
            cell.StoreProduct(_food);

            Assert.HasCount(1, cell.Products);
            Assert.AreEqual(10, cell.Products[0].ProductAmount);
            Assert.AreEqual(101, cell.Products[0].Id);
        }



        [TestMethod]
        public void StoreProduct_MergesAmounts_WhenSameProductId()
        {
            var cell = new Cell(50, 50);
            cell.StoreProduct(_stone);

            cell.StoreProduct(new Material(_stone.Id, _stone.Name, 5));

            Assert.HasCount(1, cell.Products);
            Assert.AreEqual(15, cell.Products[0].ProductAmount);
        }


        [TestMethod]
        public void StoreProduct_ThrowsWhenNotEnoughSpace()
        {
            var cell = new Cell(10, 50);

       
            Assert.ThrowsExactly<NotEnoughFreeSpaceException>(() => cell.StoreProduct(_cloth));
        }

        [TestMethod]
        public void RemoveProduct_PartialRemovalReducesStoredAmount()
        {
            var cell = new Cell(50, 50);
            cell.StoreProduct(_food);

            var removed = cell.RemoveProduct(new Food(101, "Burger", 5));

            Assert.AreEqual(5, removed.ProductAmount);
            Assert.AreEqual(5, cell.Products[0].ProductAmount);
        }

        [TestMethod]
        public void RemoveProduct_ThrowsWhenProductNotAvailable()
        {
            var cell = new Cell(50, 50);

            var request = new Food(999, "FoodTest", 1);
            Assert.ThrowsExactly<NoProductOnCellException>(() => cell.RemoveProduct(request));
        }

        [TestMethod]
        public void RemoveProduct_ThrowsWhenNotEnoughProductAvailable()
        {
            var cell = new Cell(50, 50);
            cell.StoreProduct(_stone);

            var orderProduct = new Material(202, "Stone", 100);

            Assert.ThrowsExactly<NotEnoughProductException>(() => cell.RemoveProduct(orderProduct));
        
        }
    }
}
