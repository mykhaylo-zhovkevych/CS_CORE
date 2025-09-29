using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    public class Warehouse
    {
        private readonly List<Cell> _cells = new();
        public IReadOnlyList<Cell> Cells => _cells;
        private string _name;
        private string _location;

        public Warehouse(string name, string location)
        {
            this._name = name;
            this._location = location;
        }

        public string Name { get => _name; set => _name = value; }
        public string Location { get => _location; }

        public void AddCell(Cell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            if (_cells.Any(c => c.Id == cell.Id)) throw new ArgumentException($"Cell with id {cell.Id} already exists.");
            _cells.Add(cell);
        }

        public void PrintAllCellState()
        {
            Console.WriteLine("Warehouse state:");
            foreach (var cell in _cells)
            {
                Console.WriteLine(cell.ToString());
            }   
        }
    }
}