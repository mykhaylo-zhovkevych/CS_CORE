using System;

namespace ConsoleApp4
{
    public class Program
    {
        static void Main(string[] args)
        {
            string randString = "This is a string";

            Console.WriteLine("String Length : {0}", randString.Length);
            Console.WriteLine("String Contains is : {0}", randString.Contains("is"));
            Console.WriteLine("Index of is : {0}", randString.IndexOf("is"));
            Console.WriteLine("Remmove String : {0}", randString.Remove(10, 6));
            Console.WriteLine("Insert String : {0} ", randString.Insert(10, "short"));
            Console.WriteLine("Replace String : {0}", randString.Replace("string", "sentence"));
            Console.WriteLine("Compare A to B : {0}", String.Compare("A", "B", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("|-------------------------------------------------|");
            Console.WriteLine("A = a : {0}", 
                String.Equals("A", "a", 
                StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Pad Left : {0}", 
                randString.PadLeft(20, '.'));
            Console.WriteLine("Trim : {0}", randString.Trim());
            Console.WriteLine("Uppercase : {0}", randString.ToUpper());
            Console.WriteLine("Lowercase : {0}", randString.ToLower());

            string newString = String.Format("{0} saw a {1} {2} in the {3}", 
                "Paul", "rabbit", "eating", "field");
            Console.WriteLine(newString + "\n");
            /* Escape characters are special sequences in strings that represent characters that cannot be typed directly or have special meanings. 
            Here are some common escape characters in C#: \n	Newline \t	 Tab */
            // verbatim strings are string literals that are prefixed with the @ symbol. They allow you to include special characters
            Console.WriteLine(@"Exactly What I Typed | \n \t \n \t");

        }
    }
}