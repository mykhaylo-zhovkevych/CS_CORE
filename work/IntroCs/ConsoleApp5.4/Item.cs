using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public abstract class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public Guid? ReservedBy { get; set; }
        public bool IsReserved => ReservedBy.HasValue;

        public Item(Guid id, string name)
        {
            Id = Guid.NewGuid();
            Name = name;

        }
    }
}
