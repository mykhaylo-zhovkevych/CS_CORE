using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2
{
    internal abstract class Object
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  double BasicPrice { get; set; }
        public double Surcharge { get; set; }
        public bool IsReserved { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReservationStart { get; set; }
        public DateTime? ReservationEnd { get; set; }

        public double CalcObjectPrice()
        {
            return BasicPrice * Surcharge;
        }

        public Object(Guid id, string name, double basicPrice, double surcharge)
        {
            Id = Guid.NewGuid();
            Name = name;
            BasicPrice = basicPrice;
            Surcharge = surcharge;
            IsReserved = false;
            ReturnDate = null;
            BorrowDate = null;
            ReservationStart = null;
            ReservationEnd = null;
        }
    }
}
