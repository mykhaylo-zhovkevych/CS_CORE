using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Burger
    {
        private int _avaliableBurgers;

        public string BurgerName { get; private set; }
        public decimal BurgerPrice { get; private set; }

        public int AvaliableBurgers => _avaliableBurgers;


        public Burger(string burgerName, decimal burgerPrice)
        {
            BurgerName = burgerName;
            BurgerPrice = burgerPrice;
            _avaliableBurgers++;

        }
    }
}