using ConsoleApp5._4;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    public class Borrowing
    {
        private int _remainingExtensionCredits;
        public User User { get; init; }
        public Item Item { get; init; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        // May be null if not returned back
        public DateTime? ReturnDate { get; internal set; }
        public bool IsReturned => ReturnDate.HasValue;
        // Computed property
        public int ExtentionCredits => User.Extensions;
        public int RemainingExtensionCredits => _remainingExtensionCredits;


        // Ensures valid start state
        public Borrowing(User user, Item item, DateTime loanDate,  DateTime dueDate)
        {
            User = user;
            Item = item;
            LoanDate = loanDate;
            DueDate = dueDate;

            _remainingExtensionCredits = ExtentionCredits;
        }

        public Result Extend(int months = 1)
        {
            if (months <= 0)
            {
                return Result.Fail("Extension months must be greater than zero");
            }

            if (Item.IsReserved)
            {
                throw new IsAlreadyReservedException(User, Item);
            }

            if (_remainingExtensionCredits <= 0)
            {
                return Result.Fail($"{User.Name} has no more remaining extension credits");
            }
            DueDate = DueDate.AddMonths(months);
            _remainingExtensionCredits--;

            return Result.Notify($"Item was successfully extended");
        }

        public override string ToString()
        {
            return $"User: {User.Name}, Item: {Item.Name}, LoanDate: {LoanDate}" +
                $", DueDate: {DueDate}, Returned Date: {ReturnDate}" +
                $", Returned: {IsReturned}";
        }
    }
}