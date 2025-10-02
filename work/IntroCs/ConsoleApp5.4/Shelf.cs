using ConsoleApp5._2;
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
        private List<Item> _items;


        public Shelf(int shelfNumber)
        {
            ShelfId = shelfNumber;
            _items = new List<Item>();
        }

        public void AddItemToShelf(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItemFromShelf(Item item)
        {
            _items.Remove(item);
        }

        // Returns all items from one specific shelf
        public List<Item> GetCurrentItems()
        {
            return _items;
        }
    }
}
