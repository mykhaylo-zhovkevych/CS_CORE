namespace AdapterStockApp
{
    public class StockDataProvider : IReporter
    {
        // XML formatted stock data
        private string _testData = @"<employees>
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

        public string GetReport()
        {
            Console.WriteLine("Getting Stock Data Report...");
            return _testData;
        }
    }
}