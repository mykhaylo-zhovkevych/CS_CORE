using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    internal class Warehouse
    {
        private string _name;
        private string _location;
        private List<Cell> cells = new List<Cell>();
        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location; }


        public Warehouse(string name, string location, int defaultCellCount = 10)
        {
            _name = name;
            _location = location;
            for (int i = 1; i <= defaultCellCount; i++)
            {
                cells.Add(new Cell(i));
            }
        }


        public class Product
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
}