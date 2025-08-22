using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3
{
    internal class Product
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public int ProductAmount { get; set; }

        public Product(int ProductNumber, string Name, int ProductAmount)
        {
            this.ProductNumber = ProductNumber;
            this.Name = Name;   
            this.ProductAmount = ProductAmount;
        }
    }
}