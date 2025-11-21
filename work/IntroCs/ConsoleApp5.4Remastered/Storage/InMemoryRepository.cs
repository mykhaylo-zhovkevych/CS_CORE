using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.HelperClasses;
using ConsoleApp5._4Remastered.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Storage
{
    public class InMemoryRepository : IUserRepository, IItemRepository, IBorrowingsRepository
    {
        private readonly Library _library;

        public InMemoryRepository(Library library)
        {
            _library = library;
        }

        // Update 

        public User UpdateUserProfile(Guid id, UserType newType)
        {
            return _library.UpdateUserProfile(id, newType);
        }

        // Create 

        public (bool Success, string Message) AddBorrowing(User user, Item item)
        {
            return _library.BorrowItem(user, item);
        }

        public void AddUserToInMemory(User user)
        {
            _library.Users.Add(user);
        }
        
        // Get 

        public Shelf? GetShelfById(int id)
        {
            return _library.Shelves
                .FirstOrDefault(s => s.ShelfId == id);
        }

        public User? GetExistingUserById(Guid id)
        {
            return _library.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public User? GetExistingUser(string name, UserType userType)
        {
            return _library.Users
                .FirstOrDefault(u => u.Name == name && u.UserType == userType);
        }

        public Item? GetExistingItem(string name, ItemType itemType)
        {
            return _library.Shelves
                .SelectMany(s => s.Items)
                .FirstOrDefault(i => i.Name == name && i.ItemType == itemType);
        }

        public (Item?, User?) GetPossibleBorrowing(Guid userId, Guid itemId)
        {
            var user = _library.Users
                .FirstOrDefault(u => u.Id == userId);

            var item = _library.Shelves
                .SelectMany(s => s.Items)
                .FirstOrDefault(i => i.Id == itemId);

            return (item, user);
        }

        public List<Borrowing> GetActiveBorrowings(Guid userId)
        {
            return _library.Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }

      
    }

}
