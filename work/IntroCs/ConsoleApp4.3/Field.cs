using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal class Field
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public bool IsWall { get; set; } = false;
        public bool IsDoor { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public (int x, int y) DoorTarget { get; set; }

        public Field(string name) { Id = Guid.NewGuid(); Name = name; }

        public override string ToString()
        {
            return $"{Name} {(IsWall ? "[Wall]": "")}";
        }
    }
}