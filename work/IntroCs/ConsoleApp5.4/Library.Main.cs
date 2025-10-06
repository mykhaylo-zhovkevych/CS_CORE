using ConsoleApp5._2;
using ConsoleApp5._2.Objects;
using ConsoleApp5._2.Users;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using ConsoleApp5._4.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public partial class Library
    {
        private List<Shelf> _shelves;
        private List<Borrowing> _borrowings;
        private readonly IBorrowPolicyProvider _policyProvider;

        public string Name { get; set; }
        public string Address { get; set; }
        public event EventHandler<ItemEventArgs> InformReserver;

        public delegate string PerformPrintOutput(User user);
        public delegate T PerformPrintLibraryItemsOutput<T>(Admin admin);
        
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

            if (user == null) return Result<Borrowing>.Fail("Incorrect User");
            if (string.IsNullOrEmpty(searchedItem)) return Result<Borrowing>.Fail("Item name is missing");

            var item = FindItemByName(searchedItem);
            if (item == null)
            {
                throw new InvalidOperationException("Item is not found for borrowing");
            }


            // Check if item is avaliable for borrowing
            // Must it throw the error message or can i bypass more elegantly 
            if (!CheckItemAvailability(user, item))
            {
                throw new InvalidOperationException($"{searchedItem} is currently not available for borrowing");
            }

            // Will from here the error from above catched in this block? 
            BorrowPolicy policy;
            try
            {
                policy = _policyProvider.GetPolicy(user, item);
            }
            catch (Exception ex)
            {
                return Result<Borrowing>.Fail($"Policy-Fehler: {ex.Message}");
            }

            // Is there any better way of assigning the local properties with actula?
            
            // in user define
            user.LoanFees = policy.Fees;
            user.Extensions = policy.Extensions;

            var borrowing = new Borrowing()
            {
                user = user,
                item = item,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(policy.LoanPeriod),
            };

            _borrowings.Add(borrowing);
            item.IsBorrowed = true;

            return Result<Borrowing>.Ok(borrowing, "Saved");
        }

        public Result<Borrowing> ReturnItem(User user, string currentItem)
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

            if (borrowing.item.IsReserved)
            {
                OnInformReserver(new ItemEventArgs($"The {borrowing.item.Name} is now available", borrowing.item, borrowing.user));
            }

            return Result<Borrowing>.Ok(borrowing, "Item was successfully returned");
        }


        // Is this good idea to have method that dont needs a paramater but in internally there is function that needs that paramaters? 
        public void ReserveItem(User user, string currentItem)
        {

            var item = FindItemByName(currentItem);
            if (item == null || user == null)
            {
                throw new InvalidOperationException("Item is not available for borrowing");
            }
            if (!CheckItemAvailability(user, item))
            {
                throw new InvalidOperationException($"{currentItem} is currently not available for reserving");
            }

            item.ReservedBy = user.Id;

        }


        public void CancleReservation(User user, string currentItem)
        {
            var reservetedItem = FindItemByName(currentItem);
            if (reservetedItem == null || user == null)
            {
                throw new InvalidOperationException("Item or User is missing for reservetion cancelation");
            }

            // automaticaly makes IsReserved false
            reservetedItem.ReservedBy = default;

        }

        public void ExtendBorrowingPeriod(User user, string currentItem)
        {
            var reservedItem = FindItemByName(currentItem);

            // Check if it is not reserved by someone else 
            if (!reservedItem.IsReserved && reservedItem.IsBorrowed)
            {
                var borrowing = _borrowings.FirstOrDefault(b =>
                    b.user.Id == user.Id &&
                    b.item.Name == reservedItem.Name);

                if (user.Extensions <= 0)
                {
                    borrowing.DueDate = borrowing.DueDate.AddMonths(1);
                    user.Extensions--;
                }
            }
            else if (reservedItem.IsReserved)
            {
                throw new IsAlreadyReservedException(user, reservedItem);
            }
        }

        public string ShowActiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = _borrowings.Where(b => 
                b.ReturnDate == null && 
                b.user.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.AppendLine($"{user.Name} has no active borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.AppendLine($"{user.Name} has '{b.item.Name}' from {b.LoanDate} until {b.DueDate}");
                }
            }

            return sb.ToString();
        }
        public string ShowInactiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = _borrowings.Where(b =>
                b.ReturnDate != null &&
                b.user.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.AppendLine($"{user.Name} has no past borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.AppendLine($"{user.Name} had '{b.item.Name}' from {b.LoanDate} until {b.DueDate} that was returned at {b.ReturnDate}");
                }
            }
            return sb.ToString();

        }

        // This methods for library management
        public string ShowVideoGameWithSpecificAgeRatingInLibrary(Admin admin)
        {
            StringBuilder sb = new StringBuilder();
            var allItems = getAllItemsFromShelves();

                                // Filters specific type
            var allVideoGame = allItems.OfType<VideoGame>()
                .Where(vg => 
                    vg.Genre == Enum.GameType.RPG &&
                    vg.AgeRating >= 18
                );

            foreach (var vb in allVideoGame)
            {
                sb.AppendLine($"{vb.Name}, {vb.Genre}, {vb.AgeRating}, Is this game borrowed {vb.IsBorrowed}");
                
            }

            return sb.ToString();
        }

        public int CountAllBooksInLibrary(Admin admin)
        {            
            var allItems = getAllItemsFromShelves();
            var allBooksCount = allItems.Where(i =>
                i.GetType() == typeof(Book));

            return allBooksCount.Count();

        }

        public string ShowAllItemsWithSameName(Admin admin)
        {
            StringBuilder sb = new StringBuilder();

            var allItems = getAllItemsFromShelves();
            var allItemsWithSameName = allItems.Where(n => 
                n.Name == n.Name &&
                n.Equals(n));

            foreach (var n in allItemsWithSameName)
            {
                sb.AppendLine(n.Name);
            }
                

            return sb.ToString();
        }

    }
}