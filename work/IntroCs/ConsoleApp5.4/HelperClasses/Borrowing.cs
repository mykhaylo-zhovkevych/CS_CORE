using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    // This class doenst need a constructor, because it wont used independantly
    public class Borrowing
    {
        public User user { get; set; }
        public Item item { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        // May be null if not returned back
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        public override string ToString()
        {
            return $"User: {user.Name}, Item: {item.Name}, LoanDate: {LoanDate}, DueDate: {DueDate}, Returned: {IsReturned}";
        }


    }
}
