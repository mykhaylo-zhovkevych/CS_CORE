using AdapterStockApp.External;

namespace AdapterStockApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StockExchange sx = new StockExchange();

            sx.ShowDataFromProvider(new StockDataProvider());

            sx.ShowDataFromProvider(new StockAdapter(new AnalyticsLibraryProvider()));
        }
    }
}
