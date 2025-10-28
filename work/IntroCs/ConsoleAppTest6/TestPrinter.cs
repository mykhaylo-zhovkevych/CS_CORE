using ConsoleApp6;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest6
{
    [TestClass]
    public class TestPrinter
    {
        private Printer _printer;

        private Client _client01;
        private Client _client02;
        private Client _client03;

        private List<Order> _threeOrderList;
        private List<Order> _oneOrder;

        private Order[] order;


        [TestInitialize]
        public void Setup()
        {
            order = new Order[]
            {
                new Order("Order01","Document 1 - Project Plan", 5),
                new Order("Order02", "Document 2 - Budget Report", 5),
                new Order("Order03", "Document 3 - Meeting Minutes", 5),
                new Order("Order04", "Document 4 - Marketing Strategy", 5),
                new Order("Order05","Document 5 - Sales Data", 5),
            };

            _threeOrderList = new List<Order>();
            _threeOrderList.Add(order[0]);
            _threeOrderList.Add(order[1]);
            _threeOrderList.Add(order[2]);

            _oneOrder = new List<Order>();
            _oneOrder.Add(order[4]);

            List<Order> twoOrders = new List<Order>();
            twoOrders.Add(order[3]);
            twoOrders.Add(order[4]);


            _printer = new Printer();

            _client01 = new Client("Test Worker Laptop", _printer);
            _client02 = new Client("HR Desktop", _printer);
            _client03 = new Client("IT Desktop", _printer);
        }

        [TestMethod]
        public void TestPreProcessOrders_If_AllCorrect()
        {
            // Act
            _printer.PreProcessOrders(_threeOrderList);

            // Assert
            Assert.AreEqual(_threeOrderList.Count, _printer._orderQueue.Count);
        }

        [TestMethod]
        public void TestPreProcessOrders_If_NullReferenceException()
        {
            
            Assert.ThrowsException<NullReferenceException>(() => _printer.PreProcessOrders(null));
        }


        [TestMethod]
        public async Task TestRunAsync_When_IsCancellationRequested()
        {
            // Arrange & Act
            _printer.Start();

            _printer.StopPrinter();

            await _printer._backgroundTask;

            // Assert
            Assert.IsNotNull(_printer._backgroundTask);
            Assert.IsTrue(_printer._backgroundTask.IsCompleted);
        }


        [TestMethod]
        public async Task TestRunAsync_When_NoOrdersFound()
        {
            // Arrange & Act
            _printer.Start();

            // reloop 
            var sw = Stopwatch.StartNew();
            await Task.Delay(500);
            sw.Stop();

            // No need to keep waiting
            _printer.StopPrinter();

            Assert.IsTrue(sw.Elapsed.TotalMilliseconds >= 500);
            Assert.IsNotNull(_printer._backgroundTask);
            try
            {
                await _printer._backgroundTask;
                Assert.IsTrue(_printer._backgroundTask.IsCompleted);
            }
            catch (OperationCanceledException)
            {
               
                Assert.IsTrue(_printer._backgroundTask.IsCanceled || _printer._backgroundTask.IsFaulted);
            }
        }

        [TestMethod]
        public async Task TestRunAsync_If_AllCorrect()
        {
            // Arrange & Act
            _printer.Start();

            _printer.PreProcessOrders(_oneOrder);

            await Task.Delay(200);

            // Assert
            Assert.IsNotNull(_printer._backgroundTask);
            // After processing the queue should be empty
            Assert.AreEqual(0, _printer._orderQueue.Count);

            _printer.StopPrinter();
            try
            {
                await _printer._backgroundTask;
            }
            catch (OperationCanceledException) { }
        }

        [TestMethod]
        public async Task TestProcessOrderAsync_If_AllCorrect()
        {
            // Arrange
            var testOrder = new Order("testOrder", "food", 10);

            var method = typeof(Printer).GetMethod("ProcessOrderAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method.Invoke(_printer, [testOrder]) as Task;
            await taskObj;
            sw.Stop();

            // Assert
            Assert.IsTrue(sw.Elapsed.TotalSeconds >= 10);
        }

        [TestMethod]
        public async Task TestProcessOrderAsync_If_OrderHasNoSize()
        {
            // Arrange
            var zeroOrder = new Order("ZeroOrder", "Empty", 0);

            var method = typeof(Printer).GetMethod("ProcessOrderAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var sw = Stopwatch.StartNew();
            var taskObj = method?.Invoke(_printer, [zeroOrder]) as Task;
            // Awaits for async method to complete
            await taskObj;
            sw.Stop();

            // Assert
            // Should be effectively instantaneous (small tolerance)
            Assert.IsNotNull(taskObj);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 0.2);
        }

        [TestMethod]
        public void TestStart_If_AllCorrect()
        {
            // Act
            var task = _printer.Start();

            // Assert
            Assert.IsNotNull(_printer._backgroundTask);
        }

        [TestMethod]
        public void TestStopPrinter_If_AllCorrect()
        {
            // Act
            _printer.StopPrinter();

            // Assert
            Assert.IsTrue(_printer._cts.IsCancellationRequested);
        }
    }
}
