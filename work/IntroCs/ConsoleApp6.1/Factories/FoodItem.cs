using ConsoleApp6._1.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Factories
{
    public abstract class FoodItem : IFoodItem
    {

        public abstract string Name { get; }
        public abstract decimal Price { get; }

        public abstract IFoodFactory Factory { get; }

    }
}
