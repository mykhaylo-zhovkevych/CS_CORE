namespace AdapterStockApp
{

    public class StockExchange
    {

        public StockExchange() 
        { 
        
        }

        public void ShowDataFromProvider(IReporter reporter)
        {
            var data = reporter.CreateReport();
            Console.WriteLine(data);
        }
    }
}