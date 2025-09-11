namespace Partial_Extension
{ 

    public partial class Customer
    {
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void PrintSummary()
        {
            Console.WriteLine($"{GetFullName()}, {Age} yeats old");
        }
    }
}