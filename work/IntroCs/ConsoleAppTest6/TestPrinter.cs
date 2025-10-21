using ConsoleApp6;
using System;
using System.Collections.Generic;
using System.Linq;
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


        [TestInitialize]
        public void Setup()
        {
            Order[] order = new Order[]
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
        public void TestExecuteOrder_If_MoreThanOneOrderPresent()
        {
            // Act
            _printer.ExecuteOrder(_threeOrderList);

            // Assert
            Assert.AreEqual(3, _printer._orderQueue.Count);
            Assert.IsTrue(_printer._orderSignal.CurrentCount >= 1);
        }

        
        [TestMethod]
        public void TestPrinterConstructor_If_TokenStop()
        {
            _printer.StopPrinter();

            Assert.IsTrue(_printer._cts.IsCancellationRequested);

        }
    }
}
