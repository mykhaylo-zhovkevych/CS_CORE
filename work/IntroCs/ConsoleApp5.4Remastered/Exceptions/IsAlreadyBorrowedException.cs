using ConsoleApp5._4Remastered.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Exceptions
{
    public class IsAlreadyBorrowedException : Exception
    {
        public IsAlreadyBorrowedException(Item item)
                : base ($"Apology, but {item.Name} is non retrievable")
        {
        }
    }
}
