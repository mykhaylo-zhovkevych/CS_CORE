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
        public User? ReservedBy { get; set; }        
        public bool IsReserved => ReservedBy is not null;

        // In the base will be Id to subclasses give, what if I won't give Guid id in paramaters? 
        public Item(Guid id, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public override string ToString()
        {
            return $"Item Id: {Id}, Item Name: {Name}, IsBorrowed: {IsBorrowed}" +
                $", IsReserved: {IsReserved}, ReservedBy (Id): {ReservedBy}";
        }
    }
}
