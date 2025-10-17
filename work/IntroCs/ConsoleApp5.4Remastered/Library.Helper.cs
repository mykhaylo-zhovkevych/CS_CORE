using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered
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

        private bool CheckReservePossible(Item item)
        {
            if (!item.IsBorrowed)
                return false;

            if (item.IsReserved)
                return false;

            return true;
        }


        private List<Item> GetAllItemsFromShelves()
        {
            return Shelves.SelectMany(shelf => shelf.Items).ToList();
        }

        public void AddShelf(Shelf shelf) => Shelves.Add(shelf);

    }
}