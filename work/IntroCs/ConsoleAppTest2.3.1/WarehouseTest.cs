using ConsoleApp2._3._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest2._3._1
{
    [TestClass]
    public class WarehouseTest
    {

        private Warehouse warehouse;

        [TestInitialize]
        public void Setup()
        {
            warehouse = new Warehouse("TestName", "TestLocation");
        }

        [TestMethod]
        public void AddCell_AddsCell()
        {
            var cell = new Cell(1, 50);
            warehouse.AddCell(cell);
            Assert.AreEqual(1, warehouse.Cells.Count);
        }

        [TestMethod]
        public void AddCell_ThrowsWhenSameIdAdded()
        {
            var cell01 = new Cell(1, 50);
            var cell02 = new Cell(1, 50);

            warehouse.AddCell(cell01);
            Assert.ThrowsException<ArgumentException>(() => warehouse.AddCell(cell02));
        }

        [TestMethod]
        public void AddCell_ThrowsWhenNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.AddCell(null));
        }

        [TestMethod]
        public void PrintAllCellState_WritesStateToConsole()
        {
            var c1 = new Cell(1, 10);
            c1.StoreProduct(new Food(1, "A", 3));
            var c2 = new Cell(2, 5);
            warehouse.AddCell(c1);
            warehouse.AddCell(c2);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            warehouse.PrintAllCellState();

            var output = sw.ToString();
            Assert.IsTrue(output.Contains("Warehouse state"));
            Assert.IsTrue(output.Contains("Cell 1"));
            Assert.IsTrue(output.Contains("Cell 2"));
        }
    }
}
