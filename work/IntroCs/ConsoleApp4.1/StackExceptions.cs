using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._1
{
    public class StackExceptions
    {

        public class StackFullException : Exception
        {
            public StackFullException() { }
            public StackFullException(string message) : base(message) { }
        }

        public class StackEmptyException : Exception
        {
            public StackEmptyException() { }
            public StackEmptyException(string message) : base(message) { }
        }
    }
}
