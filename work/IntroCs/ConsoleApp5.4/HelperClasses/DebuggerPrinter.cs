using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    // This class is static because it dont needs any initialisation, and it is helping class
    public static class DebuggerPrinter
    {
        // This method allows any type of input from Result to print out 
        public static string PrintOutput<T>(Result<T>result) 
        {
            var prefix = result.Success ? "[CORRECT]" : "[FALSE]";

            if (result.Data != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(prefix);
                sb.AppendLine(result.Data.ToString());
                sb.Append($"{result.Message}\n");

                return sb.ToString();
            }
            else
            {
                return $"{prefix} No data was found: {result.Message}";
            }
        }
    }
}