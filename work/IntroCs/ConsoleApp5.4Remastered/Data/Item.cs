using ConsoleApp5._4Remastered.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Data
{
    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; }
        public bool IsBorrowed { get; set; } = false;
        public User? ReservedBy { get; set; }
        public bool IsReserved => ReservedBy is not null;

        public ItemType Type { get; init; }



        public Item(string name)
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
