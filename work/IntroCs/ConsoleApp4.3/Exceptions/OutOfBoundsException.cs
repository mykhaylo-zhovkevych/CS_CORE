using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Exceptions
{
    public class OutOfBoundsException : Exception
    {
        public (int x, int y) AttemptedPosition { get; }

        public OutOfBoundsException((int x, int y) pos) : base($"Attempted to move out of bounds to position {pos}.")
        {
            AttemptedPosition = pos;
        }


    }
}
