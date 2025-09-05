namespace Inheritance
{
    internal class Vehicle
    {
        public string Brand { get; set; }
        public string Name { get; set; }

        public int Year { get; set; }

        public void Start()
        {
            Console.WriteLine("Vehicle is starting.");
        }

        public void Stop()
        {
            Console.WriteLine("Vehicle is stopping.");
        }

        static void Main(string[] args)
        {
           // Inheritance involves creating new classes (subclasses or derived classes) based on existing classes (superclasses or based classes).
           // Subclasses inherit properties and behaviours from their superclasses and can also add new features or override existing ones. Inheritance is offten described in terms of an "is-a" relationship.

            var car = new Car();

            car.Brand = "Ford";
            car.Start();
            car.Stop();

            car.NumberOfDoors = 4;
        }
    }
}
