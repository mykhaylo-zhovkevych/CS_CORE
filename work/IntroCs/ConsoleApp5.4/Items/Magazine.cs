using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Items
{
    public class Magazine : Item
    {
        public int AgeRating { get; set; }
        public string Publisher { get; set; }

        public Magazine(string name, string publisher, int ageRating): base(name)
        {
            Publisher = publisher;
            AgeRating = ageRating;
        }
    }
}
