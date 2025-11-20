using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StreamDemo
{
    public class VideoCodeExamples
    {
        public void SomeFileThings()
        {
            string filename = "text.txt";


            File.Create(filename).Close();
            if (!File.Exists(filename))
            {
                File.Create(filename);
                SomeFileThings();
            }
            else if (File.Exists(filename))
            {
                string text = "Hello, World!" + "\n";
                File.WriteAllText(filename, text);

                Console.WriteLine(File.ReadAllText(filename));

                string[] messages = { "Monday", "Jan 23, 2022" + "\n" };
                File.AppendAllLines(filename, messages);
                Console.WriteLine(File.ReadAllText(filename));
            }
        }
    }

    class FileStreamStuff
    {
        public void SomeFileStreamThings()
        {
            FileStream fs = new FileStream("text.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            string text = "This is some text written to Text.txt" + "\n nammed text.txt using FileStream class.";
            byte[] writeArr = Encoding.UTF8.GetBytes(text); // Store the text in a byte array with UTFC8 encoding
            fs.Write(writeArr, 0, text.Length); // Using the Write method to write the byte array to the textfiel
            fs.Close();

            // Another Example with FileStream and StreamReader

            StreamReader reader = new StreamReader("Text.txt");
            int lineNumber = 0; // Line number to be read from the text file 
            string line = reader.ReadLine(); // Read the lines from the text file
            while (line != null)
            {
                lineNumber++;
                Console.WriteLine("Line {0}: {1}", lineNumber, line);
                line = reader.ReadLine();
            }
            reader.Close();

            FileStream fs2 = new FileStream("Text3.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sr1 = new StreamWriter(fs2);
            sr1.WriteLine("\n" + "Writing data intop file using stream writer.");
            sr1.Close(); fs2.Close();


            string Fdata;
            FileStream fs3 = new FileStream("Text3.txt", FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader sr2 = new StreamReader(fs3))
            {
                Fdata = sr2.ReadToEnd();
                sr2.Close();
            }
            Console.WriteLine(Fdata);
            fs3.Close();
        }
    }
}
