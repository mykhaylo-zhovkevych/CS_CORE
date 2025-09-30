using System.Text.RegularExpressions;

namespace ConsoleApp5._2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var gen = new Generator();

            string random = gen.GenerateString(23);
            string randomCustom = gen.GenerateString(20, "ABCvv");

            Console.WriteLine("Random: " + random);
            Console.WriteLine("Custom: " + randomCustom);

            string reversed = random.ReverseInput();
            Console.WriteLine("Reversed: " + reversed);

        }
    }
}
