using Microsoft.VisualBasic;
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
        // Does not have any set
        public bool IsReserved => ReservedBy.HasValue;

        public Item(Guid id, string name)
        {
            Id = Guid.NewGuid();
            Name = name;

        }

        public override string ToString()
        {
            return $"User Id: {Id}, Item Name: {Name}, IsBorrowed: {IsBorrowed}" +
                $", ReservedBy (Id): {ReservedBy}, IsReserved: {IsReserved}";
        }
    }
}
