namespace Partial_Extension
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>
            {
                new Customer("Anna", "Zimmer", 22),
                new Customer("Bernd", "Maier", 45),
                new Customer("Clara", "Schmidt", 30),
                new Customer("David", "Becker", 17)
            };

            Console.WriteLine("All Customers:");
            foreach (var c in customers)
                c.PrintSummary();

            Console.WriteLine("\nCustomers from 25 years Extension Method");
                var older = customers.FIlterByMinimumAge(25);
            foreach (var c in older)
                Console.WriteLine(c.GetFullName());
        }
    }
}
