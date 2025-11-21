using ConsoleApp5._4Remastered.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Exceptions
{
    public class IsAlreadyReservedException : Exception
    {
        public IsAlreadyReservedException(Item item) 
                : base ($"Apology, but {item.Name} is allready reserved")
        {
        }
    }
}