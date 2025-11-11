using ConsoleApp6._1.Factories;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class Fries : FoodItem
    {
        public override string Name { get; } = "Fries";
        public override decimal Price { get; } = 2.50m;
        public override IFoodFactory Factory => new FriesFactory();

        public Sauce? IsCoveredOver{ get; private set; }

        public Fries() { }

        public Fries(Sauce isCoveredOver)
        {
            IsCoveredOver = isCoveredOver;

        }
    }
}
