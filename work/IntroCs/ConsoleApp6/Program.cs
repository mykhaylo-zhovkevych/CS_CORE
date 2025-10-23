using System.Collections.Concurrent;

namespace ConsoleApp6
{
    public class Program
    {
        static async Task Main()
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

            var cts = new CancellationTokenSource();
            Printer printer = new Printer();
            Client client01 = new Client("Test Worker Laptop", printer);
            Client client02 = new Client("HR Desktop", printer);
            Client client03 = new Client("IT Desktop", printer);


            await printer.StartPrinter(cts.Token);

            client01.PlacePrintOrders(twoOrders);

            client03.PlacePrintOrders(oneOrder);

            client02.PlacePrintOrders(threeOrderList);


            // printer.StopPrinter();

           
            // await client01.PlacePrintOrders(oneOrder);
            




            

            //cts.Cancel();

            // `await` puases the calling method untill the awaited task completes
            // await client03.PlacePrintIntervalOfOrdersAsync(5 ,5_000, oneOrder, cts.Token);



            // var resutl = await client01.PlacePrintOrders(twoOrders);
            //if (resutl)
            //{
            //    Console.WriteLine("placing order " + resutl);
            //}
            
            //await Task.Delay(20000);

            //Console.ReadKey();
        }
    }
}