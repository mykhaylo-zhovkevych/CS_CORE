using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2.Objects
{
    internal class Magazine : Object
    {
        public int AgeRating { get; set; }
        public string Publisher { get; set; }

        public Magazine(Guid id, string name, double basicPrice, double surcharge, int ageRating, string publisher) 
            :base(id, name, basicPrice, surcharge)
        {
            AgeRating = ageRating;
            Publisher = publisher;
        }
    }
}
