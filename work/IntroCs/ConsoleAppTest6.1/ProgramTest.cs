using ConsoleApp6._1;
using ConsoleApp6._1.Menu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest6._1
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public async Task RunSimulationAsync_When_RunOrdersInParallel()
        {
            // Arrange
            var restaurant = new Restaurant("Restaurant", "Main Station");

            var stopwatch = Stopwatch.StartNew();

            var tasks = new List<Task>
            {
                Task.Run(() => restaurant.Counters[0].OrderFood(new List<IFoodItem> { new Burger("Cheeseburger", 5.99m) })),
                Task.Run(() => restaurant.Counters[1].OrderFood(new List<IFoodItem> { new Coffe("Latte", 4.99m) }))
            };

            // Act
            await Task.WhenAll(tasks);
            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;

            // Assert
            Assert.IsTrue(elapsedMs < 2000);
        }

        [TestMethod]
        public async Task RunSimulationAsync_If_AllCorrect()
        {
            // Arrange
            var program = new Program();

            // Assert & Act
            await program.RunSimulationAsync();
        }
    }
}
