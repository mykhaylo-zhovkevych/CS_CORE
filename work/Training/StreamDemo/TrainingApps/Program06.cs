namespace TrainingApps
{

    public class Program06
    {
        public static void Main(string[] args)
        {
            // Count all characters except spaces
            // Output the result

            int count = 0;

            using (StreamReader reader = new StreamReader("message.txt"))
            {
                while (!reader.EndOfStream)
                {
                    char c = (char)reader.Read();
                    if (c != ' ')
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine($"Total characters (excluding spaces): {count}");
        }
    }
}