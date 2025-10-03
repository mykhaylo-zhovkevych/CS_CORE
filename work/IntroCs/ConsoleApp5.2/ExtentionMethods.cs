using System.ComponentModel.Design;
using System.Text;

namespace ConsoleApp5._2
{ 
    public static class ExtentionMethods
    {
        public static string ReverseInput(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input string can not null be");
            }

            char[] reversedChars = new char[input.Length];

            for (int i = 1; i <= input.Length; i++)
            {
                reversedChars[i - 1] = input[input.Length - i];
            }

            return new string(reversedChars);
        }

        //Convert to SnakeCase (each letter)
        public static string SnakeCaseInput(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input string can not null be");
            }

            var sb = new StringBuilder();
            foreach (char c in input)
            {
                // Methoden von char
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append("_");
                }
            }

            var result = sb.ToString();
            while (result.Contains("__"))
            {
                result = result.Replace("__", "_");
            }
            result = result.Trim('_');

            return result;
        }

        //truncat string
        public static string TruncateInput(this string input, int maxLength )
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input string can not null be");
            }

            if (input.Length <= maxLength)
            {
                return input;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < maxLength; i++)
            {
                sb.Append(input[i]);
            }

            sb.Append("...");
   
            return sb.ToString();
        }
    }
}