namespace AdapterStockApp
{

    public class StockExchange
    {

        public StockExchange() 
        { 
        
        }

        public void ShowDataFromProvider(IReporter reporter)
        {
            var data = reporter.GetReport();
            Console.WriteLine(data);
        }
    }
}