using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Exceptions;
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
        public List<Shelf> Shelves { get; private set; }
        public List<Borrowing> Borrowings { get; private set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public event EventHandler<ItemEventArgs>? InformReserver;
        public delegate string PerformPrintOutput(User user);


        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            Shelves = new List<Shelf>();
            Borrowings = new List<Borrowing>();
        }

        public bool BorrowItem(User user, Item item)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                if (item == null) throw new ArgumentNullException(nameof(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            try
            {
                if (!CheckBorrowPossible(item)) throw new IsAlreadyBorrowedException(user, item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            Policy policy;
            try
            {
                policy = PolicyService.GetPolicy(user, item);
            } 
            // The specific exception will be caught first 
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
            

            var allowedCredits = policy.Extensions;
            var borrowing = new Borrowing(user, item, DateTime.Now, DateTime.Now.AddDays(policy.LoanPeriod), allowedCredits);
            
            Borrowings.Add(borrowing);
            item.IsBorrowed = true;

            return true;
        }

        public bool ReturnItem(User user, Item item)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                if (item == null) throw new ArgumentNullException(nameof(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Id == item.Id &&
                !b.IsReturned);

            try
            {
                if (borrowing == null)
                {
                    throw new ArgumentException($"No entries was found with this user {user.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            borrowing.ReturnDate = DateTime.Now;
            borrowing.Item.IsBorrowed = false;

            if (borrowing.Item.IsReserved)
            {
                OnInformReserver(new ItemEventArgs($"The {borrowing.Item.Name} is now available", borrowing.Item, borrowing.User));
            }

            return true;
        }

        public bool ReserveItem(User user, Item item)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                if (item == null) throw new ArgumentNullException(nameof(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            if (!CheckReservePossible(item)) return false;

            item.ReservedBy = user;

            return true;
        }


        public bool CancelReservation(User user, Item item)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                if (item == null) throw new ArgumentNullException(nameof(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            if (item.ReservedBy != user) return false; 

            item.ReservedBy = null;

            return true;
        }

        public bool ExtendBorrowingPeriod(User user, Item item)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                if (item == null) throw new ArgumentNullException(nameof(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Name == item.Name);

            try
            {
                if (borrowing == null)
                {
                    throw new ArgumentException($"{user.Name} dont have any: {item.Name}");
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }

            return borrowing.Extend();
        }


        public string ShowActiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = Borrowings.Where(b =>
                b.ReturnDate == null &&
                b.User.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.Append($"{user.Name} has no active borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.Append($"{user.Name} has '{b.Item.Name}' from {b.LoanDate} until {b.DueDate}");
                }
            }

            return sb.ToString();
        }
        public string ShowInactiveBorrowings(User user)
        {
            StringBuilder sb = new StringBuilder();

            var borrowings = Borrowings.Where(b =>
                b.ReturnDate != null &&
                b.User.Id == user.Id)
                .ToList();

            if (!borrowings.Any())
            {
                sb.Append($"{user.Name} has no past borrowings");
            }
            else
            {
                foreach (var b in borrowings)
                {
                    sb.Append($"{user.Name} had '{b.Item.Name}' from {b.LoanDate} until {b.DueDate} that was returned at {b.ReturnDate}");
                }
            }
            return sb.ToString();

        }

        public IEnumerable<Item> QueryItems(

            string? nameContains = null,
            bool? isBorrowed = null,
            bool? isReserved = null,
            Enum.ItemType? itemType = null,
            Func<Item, bool>? customPredicate = null
        )
        {
            var items = GetAllItemsFromShelves().AsEnumerable();

            var term = nameContains?.Trim();
            if (!string.IsNullOrWhiteSpace(term))
            {
                items = items.Where(i => i.Name.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            if (isBorrowed != null)
            {
                items = items.Where(i => i.IsBorrowed == isBorrowed);
            }

            if (isReserved.HasValue)
            {
                items = items.Where(i => i.IsReserved == isReserved);
            }

            if (itemType != null)
            {
                items = items.Where(i => itemType.Equals(i.ItemType));
            }

            if (customPredicate != null)
            {
                items = items.Where(customPredicate);
            }
            return items.ToList();
        }
    }
}