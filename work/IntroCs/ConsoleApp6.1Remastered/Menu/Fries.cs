using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class Fries : IFoodItem
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Sauce? IsCoveredOver{ get; private set; }

        public Fries(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Fries(Sauce isCoveredOver)
        {
            IsCoveredOver = isCoveredOver;

        }
    }
}
