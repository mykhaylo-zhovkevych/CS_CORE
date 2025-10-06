using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    public class NotEnoughFreeSpaceException : Exception
    {

        public NotEnoughFreeSpaceException(string? message) : base(message)
        {

        }

    }
}
