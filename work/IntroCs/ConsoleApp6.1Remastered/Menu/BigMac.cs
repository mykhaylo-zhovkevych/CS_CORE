using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class BigMac : IFoodItem
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Bread? TopBread { get; private set; }
        public Bread? BottomBread { get; private set; }
        public Bacon? Bacon { get; private set; }
        public Patty? Patty { get; private set; }

        public BigMac(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public BigMac(Bread topBread, Bread bottomBread, Bacon bacon, Patty patty)
        {
            TopBread = topBread;
            BottomBread = bottomBread;
            Bacon = bacon;
            Patty = patty;
        }
    }
}