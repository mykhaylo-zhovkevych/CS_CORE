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
        public void TestAddCell()
        {
            // Arrange

            var cell = new Cell(1, 50);

            // Act

            warehouse.AddCell(cell);

            // Assert
            Assert.AreEqual(1, warehouse.Cells.Count);

        }

        [TestMethod]
        public void TestAddCell_WithSameId()
        {
            // Arrange
            var cell01 = new Cell(1, 50);
            var cell02 = new Cell(1, 50);

            // Assert & Act

            warehouse.AddCell(cell01);

            Assert.ThrowsException<ArgumentException>(() => warehouse.AddCell(cell02));
        }

        [TestMethod]
        public void TestPrintAllCellState()
        {
            // Arrange
            // Act
            // Assert
        }

    }
}
