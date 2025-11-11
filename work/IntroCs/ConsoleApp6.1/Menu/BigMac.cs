using ConsoleApp6._1.Factories;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Menu
{
    public class BigMac : FoodItem
    {
        public override string Name { get; } = "BigMac";
        public override decimal Price { get; } = 9.50m;

        public override IFoodFactory Factory => new BigMacFactory();

        public Bread? TopBread { get; private set; }
        public Bread? BottomBread { get; private set; }
        public Bacon? Bacon { get; private set; }
        public Patty? Patty { get; private set; }

        public BigMac() { }

        internal BigMac(Bread topBread, Bread bottomBread, Bacon bacon, Patty patty) : this()
        {
            TopBread = topBread;
            BottomBread = bottomBread;
            Bacon = bacon;
            Patty = patty;
        }

    }
}