using ConsoleApp3._1;

namespace ConsoleAppTest3._1
{
    [TestClass]
    public class TestShape
    {
        [TestMethod]
        public void Move_Shape_MustChangePlacement()
        {
            // Arrange
            Circle circle = new Circle(5.0);
            
            // Act
            circle.Move(3, 4);

            // Assert
            Assert.AreEqual(3, circle.X);
            Assert.AreEqual(4, circle.Y);
        }

        [TestMethod]
        public void Rectangle_IsSquare_ReturnsTrue()
        {
            Rectangle r = new Rectangle(5, 5);
            Assert.IsTrue(r.IsSquare());
        }

        [TestMethod]
        public void Rectangle_IsSquare_ReturnsFalse()
        {
            Rectangle r = new Rectangle(5, 7);
            Assert.IsFalse(r.IsSquare());
        }


    }
}
