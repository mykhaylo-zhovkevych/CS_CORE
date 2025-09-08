using System;

namespace lecture02
{
    public class Program
    {
        static void Main(string[] args)
        {

            /* Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("?=23_§);"); */

            Console.Clear();
            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello {name}!");


        }
    }
}