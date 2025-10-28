using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class Coffe : IFoodItem
    {
        public string Name { get; private set; }

        public decimal Price { get; private set; }


        public Coffe(string name, decimal price)
        {
            Name = name;
            Price = price;

        }
    }
}