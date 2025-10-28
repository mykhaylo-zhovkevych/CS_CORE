using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class Burger : IFoodItem
    {
        private int _avaliableBurgers;
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int AvaliableBurgers => _avaliableBurgers;


        public Burger(string burgerName, decimal burgerPrice)
        {
            Name = burgerName;
            Price = burgerPrice;
            _avaliableBurgers++;

        }
    }
}