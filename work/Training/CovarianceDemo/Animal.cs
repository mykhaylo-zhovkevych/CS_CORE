using System;
using System.Collections.Generic;

namespace CovarianceDemo
{ 

    abstract class Animal
    {
        public string Name { get; }
        public Animal(string name) => Name = name;
        public abstract void Speak();

    }

    class Dog : Animal
    {
        public Dog(string name) : base(name) {}
        public override void Speak() => Console.WriteLine($"{Name} says: Woof!");
    }

    class Cat : Animal
    {
        public Cat(string name) : base(name) { }
        public override void Speak() => Console.WriteLine($"{Name} says: Meow!");
    }

    class ReportGenerator
    {
        public void GenerateReport(IEnumerable<Animal> animals)
        {
            Console.WriteLine("=== Animal Report ===");
            foreach (var animal in animals)
            {
                animal.Speak();
            }
            Console.WriteLine("===========\n");
        }
    }
}