using ConsoleApp6;
using System.Diagnostics;
using System.Threading;

namespace ConsoleAppTest6
{
    [TestClass]
    public class TestClient
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
                new Order("Order05","Document 5 - Sales Data", 10),
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
        public void TestPlacePrintOrders_If_NoPrinterIsSelected()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Client client = new Client("No Printer Client", null);
                
                client.PlacePrintOrders(_oneOrder);
            });
        }

        [TestMethod]
        public void TestPlacePrintOrders_If_AllCorrect()
        {
            // Arrange
  
            // Act
            _client01.PlacePrintOrders(_oneOrder);
            // Assert
            Assert.IsTrue(_printer._orderQueue.Contains(order[4]));
        }

        [TestMethod]
        public async Task TestPlacePrintIntervalOfOrdersAsync_With_CancellationToken()
        {
            // Arrange
            CancellationTokenSource cts = new CancellationTokenSource();
            uint tries = 1;
            uint interval = 10;

            // Act
            cts.Cancel();

            await _client02.PlacePrintIntervalOfOrdersAsync(tries, interval, _threeOrderList, cts.Token);

            // Assert
            Assert.IsTrue(_printer._orderQueue.Count == 0);

        }

        [TestMethod]
        public async Task TestPlacePrintIntervalOfOrdersAsync_If_DelayIsCorrect()
        {
            // Arrange
            CancellationTokenSource cts = new CancellationTokenSource();
            uint repetitions = 2;
            uint intervals = 10;

            var sw = Stopwatch.StartNew();
            await _client01.PlacePrintIntervalOfOrdersAsync(repetitions, intervals, _oneOrder, cts.Token);
            sw.Stop();

            // Act
            var expected = repetitions * intervals;
            var elapsed = sw.Elapsed.TotalSeconds;

            // Assert
            // Minimum expected time
            Assert.IsTrue(elapsed >= expected);

        }


        [TestMethod]
        public async Task TestPlacePrintIntervalOfOrdersAsync_When_AllCorrect()
        {
            // Arrange
            CancellationTokenSource cts = new CancellationTokenSource();
            uint tries = 1;
            uint interval = 100;

            // Act
            await _client02.PlacePrintIntervalOfOrdersAsync(tries, interval, _threeOrderList, cts.Token);

            // Assert
            Assert.IsTrue(_printer._orderQueue.Count == 3);

        }
    }
}