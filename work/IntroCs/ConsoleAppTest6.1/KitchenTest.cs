using ConsoleApp6._1;
using ConsoleApp6._1.Menu;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleAppTest6._1
{
    [TestClass]
    public class KitchenTest
    {
        private Kitchen _kitchen;
        private Crew _kitchenCrew;

        [TestInitialize]
        public void Setup()
        {
            _kitchenCrew = new Crew();
            _kitchen = new Kitchen(_kitchenCrew);
        }


        [TestMethod]
        public async Task TestPrepareOrderAsync_If_AllCorrect()
        {
            // Arrange
            var counter = new Counter("Main Counter");
            counter.OrderFood(new List<IFoodItem> { new Burger("Cheeseburger", 5.99m) });

            // Act
            await _kitchen.PrepareOrderAsync(counter);

            // Assert
            Assert.IsTrue(counter.PendingOrders.IsEmpty);
        }

        [TestMethod]
        public void TestPrepareOrderAsync_If_PendingOrdersIsEmpty()
        {
            // Arrange
            var counter = new Counter("Main Counter");  

            // Assert & Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _kitchen.PrepareOrderAsync(counter));
        }

        [TestMethod]
        public async Task TestCheckKitchenCapacity_If_AllCorrect()
        {
            // Arrange
            // var order = new Order(123, new List<IFoodItem> { new Burger("Cheeseburger", 5.99m), new Coffe ("Latte", 6.99m) });
            var method = typeof(Kitchen).GetMethod("CheckKitchenCapacity", BindingFlags.NonPublic | BindingFlags.Instance);


            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method.Invoke(_kitchen, null) as Task;
            await taskObj;
            sw.Stop();

            // Assert
            Assert.IsTrue(sw.ElapsedMilliseconds >= 2500);
            
        }


        [TestMethod]
        public async Task TestCheckKitchenCapacity_If_BellowCapacity()
        {
           
            _kitchenCrew.members.Clear();
            _kitchen = new Kitchen(_kitchenCrew);

            var method = typeof(Kitchen).GetMethod("CheckKitchenCapacity", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method.Invoke(_kitchen, null) as Task;
            await taskObj;
            sw.Stop();

            // Assert
            Assert.IsTrue(sw.ElapsedMilliseconds >= 3500);
        }

        [TestMethod]
        public async Task TestCheckOrderSize_If_AllCorrect()
        {
            // Arrange
            var orderItems = new List<IFoodItem>
            {
                new Burger("Cheeseburger", 6.99m),
                new Burger("Cheeseburger", 6.99m),
                new Coffe("Latte", 3.50m),
                new Coffe("Latte", 3.50m)
            };

            var method = typeof(Kitchen).GetMethod("CheckOrderSize", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method.Invoke(_kitchen, [orderItems]) as Task;
            await taskObj;
            sw.Stop();

            // Assert
            Assert.IsTrue(sw.ElapsedMilliseconds >= 2500);
        }

        [TestMethod]
        public async Task TestCheckOrderSize_If_BellowCapacity()
        {
            var orderItems = new List<IFoodItem>
            {
                new Burger("Cheeseburger", 6.99m)
            };

            var method = typeof(Kitchen).GetMethod("CheckOrderSize", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method.Invoke(_kitchen, [orderItems]) as Task;
            await taskObj;
            sw.Stop();

            // Assert
            Assert.IsTrue(sw.ElapsedMilliseconds >= 500);
        }
    }
}