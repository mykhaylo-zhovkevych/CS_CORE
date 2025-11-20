using ConsoleApp5._4Remastered.Data;
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

        // From Service Layer
        public bool ItemExists(string name, ItemType itemType)
        {
            return Shelves.Any(s => s.Items.Any(i => i.Name == name && i.ItemType == itemType));
        }

        //public bool UserExists(string name, UserType userType)
        //{
        //    return Users.Any(u => u.Name == name && u.UserType == userType);
        //}

        public User UserExists(string name, UserType userType)
        {
            return Users.FirstOrDefault(u => u.Name == name && u.UserType == userType);
        }


        public (Item, User) BorrowingIsPossible(Guid userId, Guid itemId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var item = Shelves.SelectMany(s => s.Items).FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null || item.IsBorrowed)
            {
                return default;
            }

            return (item, user);
        }

        public List<Borrowing> GetActiveBorrowingsForUser(Guid userId)
        {
            return Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }

        //private List<Item> GetAllItemsFromShelves()
        //{
        //    var allItems = new List<Item>();
        //    foreach (var shelf in Shelves)
        //    {
        //        allItems.AddRange(shelf.Items);
        //    }

        //    return allItems;
        //}

        private List<Item> GetAllItemsFromShelves()
            => Shelves.SelectMany(s => s.Items).ToList();

        public void AddShelf(Shelf shelf) => Shelves.Add(shelf);

    }
}