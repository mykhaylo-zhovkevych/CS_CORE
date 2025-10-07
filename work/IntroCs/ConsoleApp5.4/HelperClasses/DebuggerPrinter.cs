using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    public static class DebuggerPrinter
    {
        // This method allows any type of input from Result to print out 
        public static void PrintOutput<T>(Result<T>result) 
        {
            string prefix;

            prefix = result.Success ? "[CORRECT]" : "[FALSE]";

            if (result.Data != null)
            {
                Console.WriteLine($"{prefix} {result.Data}");
            }
            else
            {
                Console.WriteLine($"{prefix} {result.Message}");
                Console.WriteLine("");
            }
        }
    }
}
