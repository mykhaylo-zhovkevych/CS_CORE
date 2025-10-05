using ConsoleApp5._2;
using ConsoleApp5._2.Objects;
using ConsoleApp5._2.Users;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public class Library
    {
        private List<Shelf> _shelves;
        private List<Borrowing> _borrowings;
        private readonly IBorrowPolicyProvider _policyProvider;

        public string Name { get; set; }
        public string Address { get; set; }
        
        public Library(string name, string address, IBorrowPolicyProvider policyProvider)
        {
            Name = name;
            Address = address;
            _shelves = new List<Shelf>();
            _borrowings = new List<Borrowing>();
            _policyProvider = policyProvider;
        }

        public Result<Borrowing> BorrowItem(User user, string searchedItem)
        {
            // Searches for specific Item
            var item = FindItemByName(searchedItem);
            if (item == null) 
            {
                throw new InvalidOperationException("Item is not available for borrowing, because a null.");
            }

            // Check if item is avaliable for borrowing
            // must it throw the error message or can i bypass more elegantly 
            if (!CheckItemAvailability(user, item))
            {
                // return result
                throw new InvalidOperationException($"{searchedItem} is currently not available for borrowing");
            }

            BorrowPolicy policy;
            try
            {
                policy = _policyProvider.GetPolicy(user, item);
            }
            catch (Exception ex)
            {
                return Result<Borrowing>.Fail($"Policy-Fehler: {ex.Message}");
            }

            user.LoanPeriod = policy.LoanPeriod;
            user.LoanFees = policy.Fees;
            user.Extensions = policy.Extensions;

            var borrowing = new Borrowing()
            {
                user = user,
                item = item,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(user.LoanPeriod),
            };
            _borrowings.Add(borrowing);
            item.IsBorrowed = true;

            return Result<Borrowing>.Ok(borrowing, "Saved");
        }


        // TODO: reimplement this method
        public void ReserveItem(User user, string currentItem )
        {
            var item = FindItemByName(currentItem);
            if (item == null)
            {
                throw new InvalidOperationException("Item is not available for borrowing, because a null.");
            }
            if (!CheckItemAvailability(user, item))
            {
                throw new InvalidOperationException($"{currentItem} is currently not available for reserving");
            }

            item.ReservedBy = user.Id;

        }


        public Result<Borrowing> ReturnItem (User user, string currentItem)
        {
            if (user == null) return Result<Borrowing>.Fail("Incorrect User");
            if (string.IsNullOrEmpty(currentItem)) return Result<Borrowing>.Fail("Item Name is incorrect or missing");

            var borrowing = _borrowings.FirstOrDefault(b =>
                b.user.Id == user.Id &&
                b.item.Name.Equals(currentItem, StringComparison.OrdinalIgnoreCase) &&
                !b.IsReturned);

            if (borrowing == null)
            {
                return Result<Borrowing>.Fail($"No entries was found with this user {user.Name}");
            }
            borrowing.ReturnDate = DateTime.Now;
            borrowing.item.IsBorrowed = false;

            return Result<Borrowing>.Ok(borrowing, "Item was successfully returned");
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

            // TODO: eliminate null possibility
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
