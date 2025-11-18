using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.HelperClasses;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Service
{
    public class LibraryService
    {
        private static Library _library;
        private static List<User> _users = new();
        private static List<Item> _items = new();

        public record CreateBorrowingRequest(Guid UserId, Guid ItemId);


        public LibraryService(Library library)
        {
            _library = library;

        }

        public static bool CheckIfUserExists(User user) 
        {
            var exists = _users.Any(u => 
                u.Name == user.Name && 
                u.UserType == user.UserType);

            if (exists)
            {
                return true;
            }
            _users.Add(user);
            return false;
        }


        public static bool CheckIfItemExistsAndHasShelf(Item item)
        {
            var exists = _items.Any(i =>
            i.Name == item.Name &&
            i.ItemType == item.ItemType);

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
            _items.Add(item);

            return false;
        }

        public static bool CheckIfBorrowingPossible(CreateBorrowingRequest request)
        {
            if (request is null)
            {
                return false;
            }

            var actualUser = _users.FirstOrDefault(u => u.Id == request.UserId);
            if (actualUser is null)
            {
                return false;
            }

            var selectedItem = _items.FirstOrDefault(i => i.Id == request.ItemId);
            if (selectedItem is null)
            {
                return false;
            }

            var borrowResult = _library.BorrowItem(actualUser, selectedItem);
            return true;

        }


        //[HttpGet("{id:guid}")]
        //public ActionResult<List<Borrowing>> GetBorrowingForUser([FromRoute] Guid id)
        //{
        //    // ToList() never returns a null
        //    var userBorrowings = _library.Borrowings.Where(u => u.User.Id == id && !u.IsReturned).ToList();

        //    if (userBorrowings.Count == 0)
        //    {
        //        return NotFound("No borrowings found for the user");
        //    }

        //    return Ok(userBorrowings);

        //}

        public static List<Borrowing> GetActiveBorrowingsForUser(Guid userId)
        {
            return _library.Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }


    }
}
