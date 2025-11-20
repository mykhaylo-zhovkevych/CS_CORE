namespace TrainingApps
{

    public class Program02
    {
        public static void Function02(string[] args)
        {
            // Reads content from file and prints to console

            using (StreamReader reader = new StreamReader("message.txt"))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("Content of message.txt:");
                Console.WriteLine(content);
            }


        }
    }

}