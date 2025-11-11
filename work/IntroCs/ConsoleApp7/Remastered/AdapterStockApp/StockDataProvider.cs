namespace AdapterStockApp
{
    public class StockDataProvider : IReporter
    {
        // XML formatted stock data
        public string Data { get; private set; } = @"<employees>
  <employee>
    <firstName>John</firstName> <lastName>Doe</lastName>
  </employee>
  <employee>
    <firstName>Anna</firstName> <lastName>Smith</lastName>
  </employee>
  <employee>
    <firstName>Peter</firstName> <lastName>Jones</lastName>
  </employee>
</employees>";


        public string CreateReport()
        {
            Console.WriteLine("Getting Stock Data Report...");
            return Data;
        }
    }
}