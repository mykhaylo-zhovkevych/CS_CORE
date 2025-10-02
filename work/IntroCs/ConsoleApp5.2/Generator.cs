using System.Text.RegularExpressions;

namespace ConsoleApp5._2
{
    public class Generator
    {
        public string GenerateString(int length, string? input = null)
        {
            const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            input ??= DefaultAlphabet;

            string pattern = $"^[{DefaultAlphabet}]+$";

            bool isValid = Regex.IsMatch(input, pattern);

            if (isValid)
            {
                var chars = "HelloWorldExample";
                var rnd = new Random();

                return new string(
                    Enumerable.Range(0, length)
                              .Select(i => chars[rnd.Next(chars.Length)])
                              .ToArray());
            }
            else
            {
                return "Your Alphabet is incorrect, try again";
            }
        }
    }
}
