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


        public (bool, string) BorrowItem(User user, Item item)
        {
            // If positive case scenario, than no need let user to know about it, else throw exception
            if (!CheckBorrowPossible(item))
            {
                throw new IsAlreadyBorrowedException(item);
            }
         
            var policy = PolicyService.GetPolicy(user.UserType, item.ItemType);
            

            var allowedCredits = policy.Extensions;
            var borrowing = new Borrowing(user, item, DateTime.Now, DateTime.Now.AddDays(policy.LoanPeriod), allowedCredits);
            
            Borrowings.Add(borrowing);
            item.IsBorrowed = true;

            return (true, $"{item.Name}, was secussfully borrowed, with {user.Name}");
        }

        public (bool, string) ReturnItem(User user, Item item)
        {
            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Id == item.Id &&
                !b.IsReturned);


            if (borrowing == null)
            {
                throw new ArgumentException($"No entries was found for this user {user.Name}");
            }

            borrowing.ReturnDate = DateTime.Now;
            borrowing.Item.IsBorrowed = false;

            if (borrowing.Item.IsReserved)
            {
                OnInformReserver(new ItemEventArgs($"The {borrowing.Item.Name} is now available", borrowing.Item, borrowing.User));
            }

            return (true, $"{item.Name}, was secussfully returned");
        }

        public (bool, string) ReserveItem(User user, Item item)
        {

            if (!CheckReservePossible(item))
            {
                throw new IsAlreadyReservedException(item);
            }
            
            item.ReservedBy = user;
            return (true, $"{item.Name} was secussfully reserved, with {user.Name}");
        }


        public (bool, string) CancelReservation(User user, Item item)
        {
            if (item.ReservedBy != user)
            {
                throw new ArgumentException($"{user.Name} has no reservation for {item.Name}");
            }; 

            item.ReservedBy = null;
            return (true, $"reservation for {item.Name} was successfully canceled");
        }

        public (bool, string) ExtendBorrowingPeriod(User user, Item item)
        {
           
            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Name == item.Name);

            if (borrowing == null)
            {
                throw new ArgumentException($"{user.Name} dont have any: {item.Name}");
            }

            return (borrowing.Extend(), $"{user.Name} has successfully extended the {item.Name}");
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