using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    public class Warehouse
    {
        private string _name;
        private string _location;

        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location; }

        public Warehouse(string name, string location)
        {
            _name = name;
            _location = location;
        }





        public void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }
    }
}