using ConsoleApp5._4Remastered.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Exceptions
{
    public class IsAlreadyReservedException : Exception
    {

        public IsAlreadyReservedException(User user, Item item) 
                : base ($"Apology, but {item.Name} is allready reserved by {user.Name}")
        {
        }
    }
}