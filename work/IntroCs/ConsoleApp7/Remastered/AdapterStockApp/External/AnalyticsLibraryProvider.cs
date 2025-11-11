namespace AdapterStockApp.External
{
    public class AnalyticsLibraryProvider
    {
        // JSON formatted data 
        public string Data { get; private set; } = @"{""employees"":[
  { ""firstName"":""John"", ""lastName"":""Doe"" },
  { ""firstName"":""Anna"", ""lastName"":""Smith"" },
  { ""firstName"":""Peter"", ""lastName"":""Jones"" }
]}";


        // Similar method but diffetent format 
        public string PreProcessData()
        {
            Console.WriteLine("Getting Analytics Library Data Report...");
            return Data;
        }

    }
}