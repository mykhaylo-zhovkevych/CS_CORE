using ConsoleApp6._1;
using ConsoleApp6._1.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest6._1
{
    [TestClass]
    public class CounterTest
    {

        [TestMethod]
        public void TestOrderFood_If_AllCorrect()
        {
            // Arrange
            var counter = new Counter("Main Counter");

            // Act
            counter.PendingOrders.Clear();
            counter.OrderFood(new List<IFoodItem> { new Burger("Cheeseburger", 5.99m) });

            // Assert
            Assert.IsFalse(counter.PendingOrders.IsEmpty);
            Assert.AreEqual(1, counter.PendingOrders.Count);
            Assert.IsInstanceOfType(counter.PendingOrders.First(), typeof(Order));
        }

        [TestMethod] 
        public void TestOrderFood_If_NoFood()
        {
            // Arrange
            var counter = new Counter("Main Counter");
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => counter.OrderFood(null));
        }
    }
}