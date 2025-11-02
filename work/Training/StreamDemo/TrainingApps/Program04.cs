namespace TrainingApps
{

    public class Program04
    {
        public static void Function04(string[] args)
        {
            // Reads file 
            // copies content to another file
            // must copy byte by byte 


            using (FileStream source = new FileStream("message.txt", FileMode.Open))
            using (FileStream target = new FileStream("copy.txt", FileMode.Create))
            {
                int b;
                while((b = source.ReadByte()) != -1)
                {
                    target.WriteByte((byte)b);
                }
            }

            Console.WriteLine("File copied to copy.txt");
        }
    }

}