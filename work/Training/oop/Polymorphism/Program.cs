namespace Polymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Polymorphims is the ability of an object to make many forms.
            // In this example there is one Class Vehicle that has some Methods that is Inherited by another subclasses and they override it, and by doint it polymorphism appears.

            //List<object> vehicles = new List<object>();
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles.Add(new Car { Brand = "Toyota", Model = "Camry", Year = 2002, NumberOfDoors = 4 });
            vehicles.Add(new Motorcycle { Brand = "Harley", Model = "Camry", Year = 2020 });

            foreach (var vehicle in vehicles)
            {
                // Such code is hard to read and not maintainable
                //if (vehicle is Car)
                //{
                //    var car = (Car)vehicle;
                //    car.Start();
                //} else if (vehicle is Motorcycle)
                //{
                //    var motorcycle = (Motorcycle)vehicle;
                //    motorcycle.Start();
                //}

                // This is a polymorphism, because i treat different type of object as the same. I dont worry if this is a Car, Motorcycle etc
                vehicle.Start();
            }
        }
    }
}
