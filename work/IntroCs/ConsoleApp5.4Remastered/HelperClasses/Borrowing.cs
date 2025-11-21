using ConsoleApp5._4Remastered.Storage;
using ConsoleApp5._4Remastered.Exceptions;
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
        public Policy Policy { get; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }

        public DateTime? ReturnDate { get; internal set; }
        public bool IsReturned => ReturnDate.HasValue;

        public uint RemainingExtensionCredits { get; private set; }


        public Borrowing(User user, Item item, Policy policy)
        {
            User = user;
            Item = item;
            Policy = policy;
            LoanDate = DateTime.Today;
            DueDate = LoanDate.AddDays(policy.LoanPeriodInDays);
            
            RemainingExtensionCredits = policy.Extensions;
        }

        public bool Extend()
        {
            //var days = Policy.LoanPeriodInDays;

            if (Item.IsReserved)
            {
                throw new IsAlreadyReservedException(Item);
            }

            if (RemainingExtensionCredits == 0)
            {
                return false;
            }

            DueDate.AddDays(Policy.LoanPeriodInDays);
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