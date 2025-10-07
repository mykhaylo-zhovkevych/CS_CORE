using ConsoleApp5._4.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public partial class Library
    {

        private void OnInformReserver(ItemEventArgs e)
        {
            InformReserver?.Invoke(this, e);
        }

        private bool CheckBorrowPossible(Item item)
        {
            if (item.IsBorrowed)
                return false;

            if (item.IsReserved)
                return false;

            return true;
        }

        // TODO: possible further checks if the same user 
        private bool CheckReservePossible(Item item)
        {
            if (item.IsBorrowed && item.IsReserved)
                return false;
            
            if (item.IsReserved) 
                return false;

            return true;
        }

        private Item FindItemByName(string name)
        {
            var searchedItem = getAllItemsFromShelves()
                .FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (searchedItem == null)
            {
                throw new ArgumentNullException("The searched item was not found");
            }

            return searchedItem;
        }

        /*
         *  better pattern 
            private bool TryFindItemByName(string name, out Item? foundItem) 
            {
                var allItems = getAllItemsFromShelves();
                foundItem = allItems.FirstOrDefault(
                    item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );

            return foundItem != null;

            }
         */

        private List<Item> getAllItemsFromShelves()
        {
            var allItems = new List<Item>();
            foreach (var shelf in _shelves)
            {
                allItems.AddRange(shelf.GetCurrentItems());
            }

            return allItems;
        }

        public void AddShelf(Shelf shelf) => _shelves.Add(shelf);

    }
}