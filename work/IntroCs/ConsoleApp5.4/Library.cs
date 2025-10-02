using ConsoleApp5._2;
using ConsoleApp5._2.Objects;
using ConsoleApp5._2.Users;
using ConsoleApp5._4.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public class Library
    {
        // not used _users
        private List<User> _users;
        private List<Shelf> _shelves;
        private List<Borrowing> _borrowings;
        

        public string Name { get; set; }
        public string Address { get; set; }
        
        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _users = new List<User>();
            _shelves = new List<Shelf>();
            _borrowings = new List<Borrowing>();
        }


        public void BorrowItem(User user, string currentItem)
        {
            // Searches for specific Item
            var searchedItem = FindItemByName(currentItem);
            if (searchedItem == null) 
            {
                throw new InvalidOperationException("Item is not available for borrowing, because a null.");
            }

            // Check if item is avaliable for borrowing
            // must it throw the error message or can i bypass more elegantly 
            if (!CheckItemAvailability(user, searchedItem))
            {
                throw new InvalidOperationException($"{currentItem} is currently not available for borrowing");
            }

            // Processes it
            DefineUserBorrowPolicy(user, searchedItem);

            var borrowing = new Borrowing()
            {
                user = user,
                item = searchedItem,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(user.LoanPeriod),
            };

            _borrowings.Add(borrowing);
            searchedItem.IsBorrowed = true;
        }

        public void ReserveItem(User user, string currentItem )
        {
            var searchedItem = FindItemByName(currentItem);
            if (searchedItem == null)
            {
                throw new InvalidOperationException("Item is not available for borrowing, because a null.");
            }
            if (!CheckItemAvailability(user, searchedItem))
            {
                throw new InvalidOperationException($"{currentItem} is currently not available for reserving");
            }

            searchedItem.ReservedBy = user.Id;

        }

        public void ReturnItem(User user, string currentItem)
        {
            var searchedItem = FindItemByUser(user);

            var borrowing = _borrowings.FirstOrDefault(b =>
                b.user.Id == user.Id &&
                b.item.Name.Equals(currentItem, StringComparison.OrdinalIgnoreCase) &&
                !b.IsReturned
            );

            if (borrowing == null)
            {
                throw new InvalidOperationException("No active borrowing found for this user");
            }

            borrowing.ReturnDate = DateTime.Now;
            borrowing.item.IsBorrowed = default;

        }


        private void DefineUserBorrowPolicy(User user, Item item)
        {

            (int loanDays, decimal fees, int extensions) policy = (user, item) switch
            {
                (Student, Book) => (30, 0.00m, 1),
                (Student, Magazine) => (30, 0.0m, 1),
                (Student, BoardGame) => (21, 0.0m, 1),
                (Student, VideoGame) => (21, 0.0m, 1),

                (Teacher, Book) => (30, 50.0m, 2),
                (Teacher, Magazine) => (30, 50.0m, 2),
                (Teacher, BoardGame) => (14, 50.0m, 2),
                (Teacher, VideoGame) => (14, 50.0m, 2),

                (ExternalUser, Book) => (30, 100.0m, 0),
                (ExternalUser, Magazine) => (30, 100.0m, 0),
                (ExternalUser, BoardGame) => (14, 100.0m, 0),
                (ExternalUser, VideoGame) => (14, 100.0m, 0),
                _ => throw new ArgumentNullException()
            };

            user.LoanPeriod = policy.loanDays;
            user.LoanFees = policy.fees;
            user.Extensions = policy.extensions;
        }


        private bool CheckItemAvailability(User user, Item item)
        {
            if (item.IsBorrowed)
                return false;

            if (item.IsReserved && item.ReservedBy != user.Id)
                return false;
         
            return true;
        }


        private Item FindItemByName(string name)
        {
            var allItems = getAllItemsFromShelves();
            return allItems.FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            // TODO: eliminate null possibility 
        }

        private Item FindItemByUser(User user)
        {
            var allItems = getAllItemsFromShelves();

            return allItems.FirstOrDefault(item => item.ReservedBy == user.Id);
        }


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
        public List<Borrowing> GetBorrowings() => _borrowings;

        public List<Borrowing> GetPastBorrowings(User user)
        {
            return _borrowings.Where(b => b.user.Id == user.Id && b.IsReturned).ToList();
        }


    }
}
