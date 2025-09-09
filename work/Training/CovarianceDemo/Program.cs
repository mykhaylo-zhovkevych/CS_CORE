namespace CovarianceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Dog> dogs = new List<Dog>
            {
                new Dog("Rex"),
                new Dog("Buddy")
            };

            List<Cat> cats = new List<Cat>
            {
                new Cat("Luna"),
                new Cat("Milo")
            };

            ReportGenerator generator = new ReportGenerator();

            generator.GenerateReport(dogs);
            generator.GenerateReport(cats);
        }
    }
}
