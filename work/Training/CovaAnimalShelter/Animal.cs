using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovaAnimalShelter
{
    public class Animal
    {
        public string Name { get; }

        public Animal(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Name}";
        }
    }
}