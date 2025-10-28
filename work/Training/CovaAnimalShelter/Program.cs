namespace CovaAnimalShelter
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var dogs = new List<Dog> { new Dog("rex"), new Dog("bob") };
            var cats = new List<Cat> { new Cat("luna"), new Cat("hoya") };

            // Reason why it works:
            // IEnumerable<T> is covariant (IEnumerable<out T>) so these calls are allowed
            // List<Dog> implements IEnumerable<Dog>, and because Dog is an Animal, IEnumerable<Dog> is an IEnumerable<Animal>
            // because the list implements IEnumerable<Dog>, it can be used wherever an IEnumerable<Animal> is expected

            // This is safe because `IEnumerable<T> is a producer/readonly interface`
            GenerateReport(dogs);
            GenerateReport(cats);


        }

        static void GenerateReport(IEnumerable<Animal> animals)
        {
            Console.WriteLine("Animal shelter report:");
            foreach (var a in animals)
            {
                Console.WriteLine($" - {a}");
            }
        }
    }
}