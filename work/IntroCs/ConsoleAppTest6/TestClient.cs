using ConsoleApp6;

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
                new Order("Order01","Document 1 - Project Plan"),
                new Order("Order02", "Document 2 - Budget Report"),
                new Order("Order03", "Document 3 - Meeting Minutes"),
                new Order("Order04", "Document 4 - Marketing Strategy"),
                new Order("Order05","Document 5 - Sales Data"),
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
        public void TestPlacePrintIntervalOfOrdersAsync()
        {
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
        public void TestPlacePrintOrders_If_PrinterIsSelected()
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
            uint interval = 100;

            // Act
            var placeOrdersTask = _client02.PlacePrintIntervalOfOrdersAsync(interval, _threeOrderList, cts.Token);

            await Task.Delay(150);

            //cts.Cancel();
            try
            {
                await placeOrdersTask;
            }
            catch (TaskCanceledException)
            {

            }
            // Assert
            Assert.IsTrue(_printer._orderQueue.Count >= 3);

        }
    }
}