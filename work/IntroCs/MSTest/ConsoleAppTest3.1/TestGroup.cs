using ConsoleApp3._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest3._1
{
    [TestClass]
    public class TestGroup
    {

        [TestMethod]
        public void TestAddShapeToGroup()
        {
            // Arrange
            Group group = new Group("TestGroup");
            Circle c = new Circle(2);

            // Act
            group.AddShapeToGroup(c);

            // Assert
            Assert.IsTrue(group.Shapes.Contains(c));
        }

        [TestMethod]
        public void TestMoveGroup_IfMovesAllShapes()
        {
            // Arrange & Act
            Group group = new Group("TestGroup");
            Circle c = new Circle(2);
            Triangle t = new Triangle(3, 4, 5);

            group.AddShapeToGroup(c);
            group.AddShapeToGroup(t);

            group.MoveGroup(2, 3);

            Assert.AreEqual(2, c.X);
            Assert.AreEqual(3, c.Y);

            Assert.AreEqual(2, t.X);
            Assert.AreEqual(3, t.Y);
        }

    }
}
