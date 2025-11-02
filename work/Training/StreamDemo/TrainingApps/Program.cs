using System;
using System.IO;

namespace TrainingApps
{

    public class Program
    {
        // Saves user input to a text file
        public static void Function01()
        {
            Console.WriteLine("Hi Write Something here");
            string message = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter("message.txt"))
            {
                writer.WriteLine(message);
            }
            Console.WriteLine("Message written to message.txt");
        }
    }

}