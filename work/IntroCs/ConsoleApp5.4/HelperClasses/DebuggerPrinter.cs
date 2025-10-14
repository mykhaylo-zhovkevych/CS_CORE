using System.Text;

namespace ConsoleApp5._4.HelperClasses
{
    // This class is static because it dont needs any initialisation, and it is helping class
    public static class DebuggerPrinter
    {
        // This method allows any type of input from Result to print out 
        public static string PrintOutput<T>(Result<T> result) 
        {
            var prefix = result.Success ? "[CORRECT]" : "[FALSE]";

            StringBuilder sb = new StringBuilder();

            sb.Append(prefix);
            sb.AppendLine(result.Data.ToString());
            sb.AppendLine($"{result.Message}");

            return sb.ToString();

        }

        public static string PrintOutput(Result result)
        {
            var prefix = result.Success ? "[CORRECT]" : "[FALSE]";
            return $"{prefix} {result.Message}";
        }
    }
}