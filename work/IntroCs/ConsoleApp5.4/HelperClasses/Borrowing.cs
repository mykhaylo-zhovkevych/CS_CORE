using ConsoleApp5._4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    // This class doenst need a constructor, because it wont used independantly
    public class Borrowing
    {
        public required User User { get; set; }
        public required Item Item { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        // May be null if not returned back
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        // TODO: Find better way of priting the output 
        public override string ToString()
        {
            return $"User: {User.Name}, Item: {Item.Name}, LoanDate: {LoanDate}" +
                $", DueDate: {DueDate}, Returned Date: {ReturnDate}" +
                $", Returned: {IsReturned}";
        }
    }
}
