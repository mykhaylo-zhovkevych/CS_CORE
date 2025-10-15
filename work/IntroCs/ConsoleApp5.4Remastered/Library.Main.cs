using ConsoleApp5._4Remastered.Data;
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
        // Nullable event 
        public event EventHandler<ItemEventArgs>? InformReserver;

        // It has be one Method that can decide which part me be executed
        public delegate string PerformPrintOutput(User user);


        public Library(string name, string address, IBorrowPolicyProvider policyProvider)
        {
            Name = name;
            Address = address;
            Shelves = new List<Shelf>();
            Borrowings = new List<Borrowing>();
        }



        // TODO: Exception preferred over early return 
        public bool BorrowItem(User user, Item item)
        {
            if (user == null) return Result.Fail("Incorrect User");

            // But Null than it can be neglect, because it is expected
            //if (string.IsNullOrEmpty(searchedItem)) return Result.Fail("Item name is missing");

            // Double cheking: overcoded? NO because, e.x: when no similar data in databank found than exception, because unexpected
            // var item = FindItemByName(searchedItem);

            // Expected
            if (!CheckBorrowPossible(item))
            {
                return Result.Fail($"{item.Name} is currently not available for borrowing");
            }

            // Unexpected, catches a exception
            BorrowPolicy policy = PolicyProvider.GetPolicy(user, item);

            var borrowing = new Borrowing(user, item, DateTime.Now, DateTime.Now.AddDays(policy.LoanPeriod));
            Borrowings.Add(borrowing);
            item.IsBorrowed = true;

            return Result<Borrowing>.Ok(borrowing, "Saved");
        }

        // Possible other way of implementing, as method that receives only Borrowing object
        // The Borrowing will be updated

        // better naming, like i dont return the titile but thwe whole item
        public bool ReturnItem(User user, Item item)
        {
            if (user == null) return Result.Fail("Incorrect User");
            if (string.IsNullOrEmpty(searchedItem)) return Result.Fail("Item Name is missing");

            // Unexpected, e.x. no data was found in databank
            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Name.Equals(searchedItem, StringComparison.OrdinalIgnoreCase) &&
                !b.IsReturned);

            if (borrowing == null)
            {
                throw new ArgumentException($"No entries was found with this user {user.Name}");
            }

            borrowing.ReturnDate = DateTime.Now;
            borrowing.Item.IsBorrowed = false;

            if (borrowing.Item.IsReserved)
            {
                OnInformReserver(new ItemEventArgs($"The {borrowing.Item.Name} is now available", borrowing.Item, borrowing.User));
            }
            // After successful return the old object Borrowing can be overwriten and saved in as non active
            return Result<Borrowing>.Ok(borrowing, "Item was successfully returned");
        }

        public bool ReserveItem(User user, Item item)
        {
   

            if (user == null) return Result.Fail("No User");

            var item = FindItemByName(searchedItem);
            if (!CheckReservePossible(item))
            {
                return Result.Fail($"{searchedItem} is currently not available for reserving");
            }

            item.ReservedBy = user;

            return Result.Notify("Item was successfully reserved");
        }


        public bool CancelReservation(User user, Item item)
        {
            if (user == null) return Result.Fail("No User");
            // Unexpected
            var reservetedItem = FindItemByName(searchedItem);

            // Expected: A double reinforcment of the ReserveItem method
            if (reservetedItem.ReservedBy != user)
                return Result.Fail("You cannot cancel another user's reservation");

            // Automaticaly makes IsReserved false
            reservetedItem.ReservedBy = null;

            return Result.Notify("Item was successfully cancelled");
        }

        public Result ExtendBorrowingPeriod(User user, string searchedItem)
        {
            if (user == null) return Result.Fail("Incorrect User");
            var reservedItem = FindItemByName(searchedItem);

            //Check if it is not reserved and borrowed simultaneously
            // if (!reserveditem.isreserved && reserveditem.isborrowed)

            var borrowing = Borrowings.FirstOrDefault(b =>
                b.User.Id == user.Id &&
                b.Item.Name == reservedItem.Name);

            if (borrowing == null)
            {
                throw new ArgumentException($"{user.Name} dont have any: {reservedItem}");
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
            Type? itemType = null,
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
                items = items.Where(i => itemType.IsAssignableFrom(i.GetType()));
            }

            if (customPredicate != null)
            {
                items = items.Where(customPredicate);
            }
            return items.ToList();
        }
    }




}
