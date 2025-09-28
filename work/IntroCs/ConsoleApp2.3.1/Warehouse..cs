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

        public void AddCell(Cell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            if (_cells.Any(c => c.Id == cell.Id)) throw new ArgumentException($"Cell with id {cell.Id} already exists.");
            _cells.Add(cell);
        }
    }
}