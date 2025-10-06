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
            var cell = new Cell(5, 50);

            warehouse.AddCell(cell);

            Assert.AreEqual(1, warehouse.Cells.Count);
        }

        [TestMethod]
        public void PrintAllCellState_WritesState()
        {
            var c1 = new Cell(10, 50);
            c1.StoreProduct(new Food(1, "A", 3));
            var c2 = new Cell(5, 40);
    

            warehouse.AddCell(c1);
            warehouse.AddCell(c2);

            string output = warehouse.PrintAllCellState();

            string actual = @"Warehouse state:
Cell 10 (Used 3/50): 3x A
Cell 5 (Capacity 40): empty
";

            Assert.AreEqual(output, actual);

        }
    }
}
