using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StreamDemo
{
    public class CaesarWithStream
    {
        private const string filepath = @"C:\Users\chmyzh1\Source\Repos\CS_CORE\work\Training\StreamDemo\text.txt";
        private const string fileoutput = @"C:\Users\chmyzh1\Source\Repos\CS_CORE\work\Training\StreamDemo\copy.txt";

        static void Main()
        {
            CaesarWithStream caesar = new CaesarWithStream();
            caesar.EncryptFileWithStream();
        }

        // In Constructor only string, reader or writer and the client decode and the server encode the stream data 
        public void EncryptFileWithStream()
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("File not found");
                return;
            }

            using (StreamReader reader = new StreamReader(filepath, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(fileoutput, false, Encoding.UTF8))
            {
                string? lines;
                while ((lines = reader.ReadLine()) != null)
                {
                    string encryptedLine = SwapTheChars(lines);
                    Console.WriteLine(encryptedLine);
                    writer.WriteLine(encryptedLine);
                }
            }
        }

        private string SwapTheChars(string lines)
        {
            if (string.IsNullOrEmpty(lines))
                return lines;

            int key = 3;

            var sb = new StringBuilder(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                char ch = lines[i];

                if (ch >= 'A' && ch <= 'Z')
                {
                    char enc = (char)(((ch + key - 65) % 26) + 65);
                    sb.Append(enc);
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    char enc = (char)(((ch + key - 97) % 26) + 97);
                    sb.Append(enc);
                }
              
            }

            return sb.ToString();
        }
    }
}
