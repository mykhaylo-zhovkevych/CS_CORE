using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Restaurant
    {

        public string RestaurantName { get; private set; }
        public string Location { get; private set; }

        public Kitchen Kitchen { get; private set; }
        public Crew Crew { get; private set; }
        public Counter Counter { get; private set; }

        public Restaurant(string restaurantName, string location)
        {
            RestaurantName = restaurantName;
            Location = location;
            Crew = new Crew();
            Kitchen = new Kitchen(Crew);
            Counter = new Counter("Main Counter");
        }


    }
}