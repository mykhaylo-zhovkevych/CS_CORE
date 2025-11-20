using ConsoleApp6._1.Factories;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class Coffee : FoodItem
    {
        public override string Name { get; } = "Coffee";
        public override decimal Price { get; } = 4.50m;
        public override IFoodFactory Factory => new CoffeeFactory();
        public Cookie? IsServed { get; private set; }


        public Coffee() { }

        public Coffee(Cookie isServed)
        {
            IsServed = isServed;

        }
    }
}
