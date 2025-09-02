using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2.Objects
{
    internal class Book : Object
    {
        public int Iban { get; set; }
        public string Publisher { get; set; }

        public Book(Guid id, string name, double basicPrice, double surcharge, int iban, string publisher)
            : base(id, name, basicPrice, surcharge)
        {
            Iban = iban;
            Publisher = publisher;
        }

    }
}