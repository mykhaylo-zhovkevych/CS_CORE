using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Objects
{
    internal class Magazine : Item
    {
        public int AgeRating { get; set; }
        public string Publisher { get; set; }

        public Magazine(Guid id, string name, int ageRating, string publisher): base(id, name)
        {
            AgeRating = ageRating;
            Publisher = publisher;
        }
    }
}
