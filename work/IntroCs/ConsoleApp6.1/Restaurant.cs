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
        public List<Counter> Counters { get; private set; } = new List<Counter>();


        public Restaurant(string restaurantName, string location)
        {
            RestaurantName = restaurantName;
            Location = location;
            Crew = new Crew();
            Kitchen = new Kitchen(Crew);

            Counters.Add(new Counter("Main Counter"));
            Counters.Add(new Counter("Secondary Counter"));

        }
    }
}