using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryAPI.Service
{
    public class LibraryService
    {
        private static Library _library;

        public record CreateBorrowingRequest(Guid UserId, Guid ItemId);


        public LibraryService(Library library)
        {
            _library = library;

        }

        public static bool CheckIfUserExists(User user) 
        {
            var exists = _library.Users.Any(u => 
                u.Name == user.Name && 
                u.UserType == user.UserType);

            if (exists)
            {
                return true;
            }
            _library.Users.Add(user);
            return false;
        }


        public static bool CheckIfItemExists(Item item)
        {
            var exists = _library.Shelves.Any(shelf =>
                shelf.Items.Any(i => i.Name == item.Name));

            if (exists)
            {
                return true;
            }

            var existsShelf = _library.Shelves.FirstOrDefault(s => s.ShelfId == 1000);
            if (existsShelf == null)
            {
                return true;
            }
            existsShelf.AddItemToShelf(item);
            existsShelf.Items.Add(item);
           //_library.Items.Add(item);

            return false;
        }

        public static bool CheckIfBorrowingPossible(CreateBorrowingRequest request)
        {
            if (request is null)
            {
                return false;
            }

            var actualUser = _library.Users.FirstOrDefault(u => u.Id == request.UserId);
            if (actualUser is null)
            {
                return false;
            }

            // Item selectedItem = _library.Shelves.FirstOrDefault(s => s.Items.Find(i => i.Id == request.ItemId));
            var selectedItem = _library.Shelves
                .SelectMany(s => s.Items)
                .FirstOrDefault(i => i.Id == request.ItemId);

            if (selectedItem is null)
            {
                return false;
            }

            var borrowResult = _library.BorrowItem(actualUser, selectedItem);
            return true;

        }

        public static List<Borrowing> GetActiveBorrowingsForUser(Guid userId)
        {
            return _library.Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }
    }
}
