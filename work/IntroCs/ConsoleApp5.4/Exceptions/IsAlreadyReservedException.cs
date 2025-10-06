using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Exceptions
{
    public class IsAlreadyReservedException : Exception
    {
        public string ItemName { get; }
        public string UserName { get; }

        public IsAlreadyReservedException(User user, Item item) : base ()
        {
            ItemName = item.Name;
            UserName = user.Name;

        }

        public override string ToString()
        {
            return $"Apology, but {ItemName} is allready reserved by {UserName}";
        }
    }
}
