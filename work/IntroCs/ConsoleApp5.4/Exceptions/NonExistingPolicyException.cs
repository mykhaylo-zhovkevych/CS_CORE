using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Exceptions
{
    public class NonExistingPolicyException : Exception
    {

        public NonExistingPolicyException() : this("No Policy was found.")
        {
        }

        public NonExistingPolicyException(string? message) : base(message)
        {
        }
    }
}