using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2
{
    internal abstract class Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  double BasicPrice { get; set; }
        public double Surcharge { get; set; }
        public bool IsReserved { get; set; }
        // I have feeling that I am traying to make a system to complicated again.
        // public abstract User? ReservedBy { get; set; }
        public DateTime? ReturnDate { get; set; }

        public double CalcObjectPrice()
        {
            return BasicPrice * Surcharge;
        }

        public Object(int id, string name, double basicPrice, double surcharge)
        {
            Id = id;
            Name = name;
            BasicPrice = basicPrice;
            Surcharge = surcharge;
            IsReserved = false;
            ReturnDate = null;
        }
    }
}
