using ConsoleApp5._4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public class Shelf
    {
        public int ShelfId { get; private set; }

        public List<Item> Items { get; }

        public Shelf(int shelfNumber)
        {
            ShelfId = shelfNumber;
            Items = new List<Item>();
        }

        public void AddItemToShelf(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItemFromShelf(Item item)
        {
            Items.Remove(item);
        }
    }
}