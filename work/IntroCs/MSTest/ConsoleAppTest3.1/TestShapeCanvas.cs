using ConsoleApp3._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest3._1
{
    [TestClass]
    public class TestShapeCanvas
    {

        [TestMethod]
        public void TestAddShape_IfCorrect()
        {
            // Arrange
            ShapeCanvas canvas = new ShapeCanvas();
            Circle c = new Circle(3);
            // Act
            canvas.AddShape(c);

            Assert.IsTrue(canvas.Shapes.Contains(c));
        }

        [TestMethod]
        public void TestRemoveShape_IfCorrect()
        {
            // Arrange
            ShapeCanvas canvas = new ShapeCanvas();
            Circle c = new Circle(3);
            canvas.AddShape(c);

            // Act
            canvas.RemoveShape(c);

            Assert.IsFalse(canvas.Shapes.Contains(c));
        }

        [TestMethod]
        public void TestFindByName_IfCorrect()
        {
            // Arrange
            ShapeCanvas canvas = new ShapeCanvas();
            Circle c = new Circle(3);
            canvas.AddShape(c);

            //Act
            var found = canvas.FindByName("Circle");

            Assert.AreEqual(c, found);
        }



    }
}
