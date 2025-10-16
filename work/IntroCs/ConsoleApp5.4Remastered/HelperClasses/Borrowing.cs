using ConsoleApp5._4Remastered.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.HelperClasses
{
    public class Borrowing
    {
        public User User { get; init; }
        public Item Item { get; init; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }

        public DateTime? ReturnDate { get; internal set; }
        public bool IsReturned => ReturnDate.HasValue;

        public int RemainingExtensionCredits { get; private set; }


        // Ensures valid start state
        public Borrowing(User user, Item item, DateTime loanDate, DateTime dueDate, Policy? temp)
        {
            User = user;
            Item = item;
            LoanDate = loanDate;
            DueDate = dueDate;

            RemainingExtensionCredits = temp.Extensions;
        }

        public bool Extend(int months = 1)
        {
            if (months <= 0) return false;

            if (Item.IsReserved)
            {
                throw new _4.Exceptions.IsAlreadyReservedException(User, Item);
            }

            if (RemainingExtensionCredits <= 0) return false;
            
            DueDate = DueDate.AddMonths(months);
            RemainingExtensionCredits--;

            return true;
        }

        public override string ToString()
        {
            return $"User: {User.Name}, Item: {Item.Name}, LoanDate: {LoanDate}" +
                $", DueDate: {DueDate}, Returned Date: {ReturnDate}" +
                $", Returned: {IsReturned}";
        }
    }
}
