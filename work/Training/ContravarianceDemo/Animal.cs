using System;

namespace ContravarianceDemo
{

    class Animal
    {
        public string Name { get; }
        public Animal(string name) => Name = name;
        public virtual void Speak() => Console.WriteLine($"{Name} says: I am an animal.");
    }

    class Dog : Animal
    {
        public Dog(string name) : base(name) { }
        public override void Speak() => Console.WriteLine($"{Name} says: Woof");
    }

    class Cat: Animal
    {
        public Cat(string name) : base(name) { }
        public override void Speak() => Console.WriteLine($"{Name} says: Meow");
    }

    interface ITrainer<in T>
    {

        void Train(T animal);
    }

    class GeneralTrainer : ITrainer<Animal>
    {
        public void Train(Animal animal)
        {
            Console.WriteLine($"Training {animal.Name}");
            animal.Speak();
        }
    }
}