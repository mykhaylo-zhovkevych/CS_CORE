using ConsoleApp5._4Remastered.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Storage
{
    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; }
        public bool IsBorrowed { get; set; } = false;
        public User? ReservedBy { get; internal set; }
        public bool IsReserved => ReservedBy is not null;
        public ItemType ItemType { get; set; }


        public Item(string name, ItemType itemType)
        {
            Id = Guid.NewGuid();
            Name = name;
            ItemType = itemType;
        }

        public override string ToString()
        {
            return $"Item Id: {Id}, Item Name: {Name}, IsBorrowed: {IsBorrowed}" +
                $", IsReserved: {IsReserved}, ReservedBy (Id): {ReservedBy}";
        }


    }
}
