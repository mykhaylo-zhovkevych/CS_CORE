using ConsoleApp5._4.Items;
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
        // TODO: Chnage them into Properties 
        private readonly List<Shelf> _shelves;
        private readonly List<Borrowing> _borrowings;
        private readonly IBorrowPolicyProvider _policyProvider;

        public string Name { get; set; }
        public string Address { get; set; }
        // Nullable event 
        public event EventHandler<ItemEventArgs>? InformReserver;

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
            // If it is mistyped will be cause the fail
            // It will not save the data, so called early return, and it will just return error message
            if (user == null) return Result<Borrowing>.Fail("Incorrect User");
            if (string.IsNullOrEmpty(searchedItem)) return Result<Borrowing>.Fail("Item name is missing");

            // Double cheking: overcoded
            var item = FindItemByName(searchedItem);

            // Check if item is avaliable for borrowing
            if (!CheckBorrowPossible(item))
            {
                throw new InvalidOperationException($"{searchedItem} is currently not available for borrowing");
            }

            BorrowPolicy policy = _policyProvider.GetPolicy(user, item);
            // Catches a exception if there is need to

            var borrowing = new Borrowing()
            {
                User = user,
                Item = item,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(policy.LoanPeriod),
            };

            _borrowings.Add(borrowing);
            item.IsBorrowed = true;


            return Result<Borrowing>.Ok(borrowing, "Saved");
        }

        public Result<Borrowing> ReturnItem(User user, string searchedItem)
        {
            if (user == null) return Result<Borrowing>.Fail("Incorrect User");
            if (string.IsNullOrEmpty(searchedItem)) return Result<Borrowing>.Fail("Item Name is incorrect or missing");

            var borrowing = _borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Name.Equals(searchedItem, StringComparison.OrdinalIgnoreCase) &&
                !b.IsReturned);

            if (borrowing == null)
            {
                return Result<Borrowing>.Fail($"No entries was found with this user {user.Name}");
            }

            borrowing.ReturnDate = DateTime.Now;
            borrowing.Item.IsBorrowed = false;

            if (borrowing.Item.IsReserved)
            {
                OnInformReserver(new ItemEventArgs($"The {borrowing.Item.Name} is now available", borrowing.Item, borrowing.User));
            }

            return Result<Borrowing>.Ok(borrowing, "Item was successfully returned");
        }


        public Result<Item> ReserveItem(User user, string searchedItem)
        {
            if (user == null) return Result<Item>.Fail("Incorrect User");
            var item = FindItemByName(searchedItem);

            if (!CheckReservePossible(item))
            {
                throw new InvalidOperationException($"{searchedItem} is currently not available for reserving");
            }

            item.ReservedBy = user.Id;
            // debugg
            // Console.WriteLine($"ReservedBy: {item.ReservedBy}, IsReserved: {item.IsReserved}");
            return Result<Item>.Ok(item, "Item was successfully reserved");
        }


        public Result<Borrowing> CancleReservation(User user, string searchedItem)
        {
            if (user == null) return Result<Borrowing>.Fail("No User");
            var reservetedItem = FindItemByName(searchedItem);

            // automaticaly makes IsReserved false
            reservetedItem.ReservedBy = default;

            return Result<Borrowing>.Fine("Item was successfully cancelled");
        }

        public Result<Borrowing> ExtendBorrowingPeriod(User user, string searchedItem)
        {
            if (user == null) return Result<Borrowing>.Fail("Incorrect User");
            var reservedItem = FindItemByName(searchedItem);

            // Check if it is not reserved and borrowed simultaneously
            if (!reservedItem.IsReserved && reservedItem.IsBorrowed)
            {
                var borrowing = _borrowings.FirstOrDefault(b =>
                    b.User.Id == user.Id &&
                    b.Item.Name == reservedItem.Name);

                if (borrowing == null)
                {
                    throw new ArgumentNullException($"{user.Name} dont have any: {reservedItem}");
                }
                else if (user.Extensions <= 0)
                {
                    borrowing.DueDate = borrowing.DueDate.AddMonths(1);
                    user.Extensions--;
                }
            }
            else if (reservedItem.IsReserved)
            {
                throw new IsAlreadyReservedException(user, reservedItem);
            }
            return Result<Borrowing>.Fine("Item was successfully extended");
        }

        public string ShowActiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = _borrowings.Where(b => 
                b.ReturnDate == null && 
                b.User.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.AppendLine($"{user.Name} has no active borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.AppendLine($"{user.Name} has '{b.Item.Name}' from {b.LoanDate} until {b.DueDate}");
                }
            }

            return sb.ToString();
        }
        public string ShowInactiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = _borrowings.Where(b =>
                b.ReturnDate != null &&
                b.User.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.AppendLine($"{user.Name} has no past borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.AppendLine($"{user.Name} had '{b.Item.Name}' from {b.LoanDate} until {b.DueDate} that was returned at {b.ReturnDate}");
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