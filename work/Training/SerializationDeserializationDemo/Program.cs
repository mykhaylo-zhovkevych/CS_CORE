using System.Text.Json;

namespace SerializationDeserializationDemo
{
    public class Purchase
    {
            public string? ProductName { get; set; }
            public DateTime DateTime { get; set; }
            public decimal ProductPrice { get; set; }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            //Purchase purchase = new Purchase
            //{
            //    ProductName = "Laptop",
            //    DateTime = DateTime.Now,
            //    ProductPrice = 999.99m
            //};

            //var options = new JsonSerializerOptions();

            //options.WriteIndented = true;

            //string jsonString = JsonSerializer.Serialize<Purchase>(purchase, options);

            //Non readible format but faster serialization
            //byte[] jsonStringInBytes = JsonSerializer.SerializeToUtf8Bytes<Purchase>(purchase);

            var purchaseJson = File.ReadAllText("purchase.json");

            Purchase purchase = JsonSerializer.Deserialize<Purchase>(purchaseJson);

        }
    }
}
