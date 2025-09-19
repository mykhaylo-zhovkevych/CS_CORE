using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal abstract class Field
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public abstract char Symbol { get; }
        public List<Item> Items { get; } = new List<Item>();

        public virtual bool CanEnter { get; set; }
        public virtual bool MovePlayerToField(Player player) { return CanEnter; }

        public Field(string name) { Id = Guid.NewGuid(); Name = name; }

    }
}