using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Program
    {
        static async Task Main()
        {
            Order[] order = new Order[]
            {
                    new Order("Order01","Document 1 - Project Plan", 5),
                    new Order("Order02", "Document 2 - Budget Report", 5),
                    new Order("Order03", "Document 3 - Meeting Minutes", 5),
                    new Order("Order04", "Document 4 - Marketing Strategy", 5),
                    new Order("Order05","Document 5 - Sales Data", 5),
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

            var cts = new CancellationTokenSource();


            Printer printer = new Printer();
            await printer.Start();


            Client client01 = new Client("Test Worker Laptop", printer);
            Client client02 = new Client("HR Desktop", printer);
            Client client03 = new Client("IT Desktop", printer);

            //client01.PlacePrintOrders(twoOrders);
            //client02.PlacePrintOrders(threeOrderList);


            // `await` puases the calling method untill the awaited task completes
            Task interval = client03.PlacePrintIntervalOfOrdersAsync(2 ,10, oneOrder, cts.Token);
            
            client03.PlacePrintOrders(threeOrderList);


            //printer.StopPrinter();

            // While it is running, the request cancelationtoken is not checked until the next iteration 
            printer.RequestCancelationAfterSomeTime(5);
            cts.Cancel();
         


            Console.ReadKey();


            // This is a Background Thread, transforted into foreground thread, makes wait before exiting Main thread
            //await printer._backgroundTask;

        }
    }
}