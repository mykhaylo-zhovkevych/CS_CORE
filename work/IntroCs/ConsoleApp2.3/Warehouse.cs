using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3
{
    internal class Warehouse
    {
        private string _name;
        private string _location;
        private List<Cell> cells = new List<Cell>();

        public Warehouse(string name, string location)
        {
            this._name = name;
            this._location = location;
        }

        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location;  }

        // Hilfsfunktion zum Hinzufügen von Zellen 
        public void AddCell(Cell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell), "Cell cannot be null.");
            }
            if (cells.Any(c => c.CellNumber == cell.CellNumber))
            {
                throw new ArgumentException($"Cell with number {cell.CellNumber} already exists.");
            }
            cells.Add(cell);
        }
    }
}
