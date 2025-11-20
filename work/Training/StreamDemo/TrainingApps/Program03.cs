namespace TrainingApps
{

    public class Program03
    {
        public static void Functio03(string[] args)
        {
            // Reads file line by line 
            // Counts the lines
            // prints the total lines of code 
            int lineCount = 0;

            using (StreamReader reader = new StreamReader("message.txt"))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            Console.WriteLine($"Total lines of code: {lineCount}"); 
        }
    }

}