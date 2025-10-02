using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ConsoleApp5._2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gen = new Generator();

            string random = gen.GenerateString(23);
            string randomCustom = gen.GenerateString(20, "ABCvv");

            string nonrandom = gen.GenerateString(23);

            Console.WriteLine(nonrandom);

            Console.WriteLine("Random: " + random);
            Console.WriteLine("Custom: " + randomCustom);

            string reversed = random.ReverseInput();
            Console.WriteLine("Reversed: " + reversed);

            string testString = "Hello this is a Demo string!";
            string snake = testString.SnakeCaseInput();
            Console.WriteLine(snake);

            string turnacate = testString.TruncateInput(15);
            Console.WriteLine(turnacate);

        }
    }
}
