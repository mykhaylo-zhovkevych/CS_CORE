using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2
{
    public abstract class Object
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool IsBorrowed { get; set; }
        public bool IsReserved { get; set; }

        public int ReservationExtensionCount { get; set; } = 0;


        public Object(Guid id, string name, double basicPrice, double surcharge)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsBorrowed = false;
            IsReserved = false;

        }
    }
}
