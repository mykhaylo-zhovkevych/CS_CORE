using ConsoleApp5._4Remastered.Storage;
using ConsoleApp5._4Remastered.Enum;
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

        //public Item? GetExistingItem(string name, ItemType itemType)
        //{
        //    return Shelves
        //        .SelectMany(s => s.Items)
        //        .FirstOrDefault(i => i.Name == name && i.ItemType == itemType);
        //}

        //public User? GetExistingUserById(Guid id)
        //{
        //    return Users
        //        .FirstOrDefault(u => u.Id == id);
        //}


        //public User? GetExistingUser(string name, UserType userType)
        //{
        //    return Users
        //        .FirstOrDefault(u => u.Name == name && u.UserType == userType);
        //}


        public (Item?, User?) GetPossibleBorrowing(Guid userId, Guid itemId)
        {
            var user = Users
                .FirstOrDefault(u => u.Id == userId);

            var item = Shelves
                .SelectMany(s => s.Items)
                .FirstOrDefault(i => i.Id == itemId);

            return (item, user);
        }

        public List<Borrowing> GetActiveBorrowings(Guid userId)
        {
            return Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }


        private List<Item> GetAllItemsFromShelves()
            => Shelves.SelectMany(s => s.Items).ToList();

        //private List<Item> GetAllItemsFromShelves()
        //{
        //    var allItems = new List<Item>();
        //    foreach (var shelf in Shelves)
        //    {
        //        allItems.AddRange(shelf.Items);
        //    }

        //    return allItems;
        //}

        public void AddShelf(Shelf shelf) => Shelves.Add(shelf);

    }
}