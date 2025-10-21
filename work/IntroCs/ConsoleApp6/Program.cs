using System.Collections.Concurrent;

namespace ConsoleApp6
{
    public class Program
    {
        static void Main(string[] args)
        {

            Order[] order = new Order[]
            {
                new Order("Order01","Document 1 - Project Plan"),
                new Order("Order02", "Document 2 - Budget Report"),
                new Order("Order03", "Document 3 - Meeting Minutes"),
                new Order("Order04", "Document 4 - Marketing Strategy"),
                new Order("Order05","Document 5 - Sales Data"),
            };

            List<Order> threeOrderList = new List<Order>();
            threeOrderList.Add(order[0]);
            threeOrderList.Add(order[1]);
            threeOrderList.Add(order[2]);

            List<Order> oneOrder = new List<Order>();
            oneOrder.Add(order[4]);

            List<Order> twoOrders = new List<Order>();
            twoOrders.Add(order[3]);
            twoOrders.Add(order[4]);


            Printer printer = new Printer();

            Client client01 = new Client("Test Worker Laptop", printer);
            Client client02 = new Client("HR Desktop", printer);
            Client client03 = new Client("IT Desktop", printer);


            //client01.AddOrders(oneOrder);
            //client02.AddOrders(threeOrderList);

            //client03.AddOrder(orders[3]);
            //client03.AddOrder(orders[4]);

            // printer.StopPrinter();

            client01.PlacePrintOrders(oneOrder);
            
            client02.PlacePrintOrders(threeOrderList);
            //client03.PlacePrintOrders();

            var cts = new CancellationTokenSource();

            client03.PlacePrintIntervalOfOrdersAsync(5_000, twoOrders, cts.Token).Wait();

        }
    }
}
