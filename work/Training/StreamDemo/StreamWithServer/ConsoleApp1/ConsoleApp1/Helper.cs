using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Helper
    {

        public static string Encrypt(string text)
        {
            return Ceaser(text, 3);
        }

        public static string Decrypt(string text)
        {
            return Ceaser(text, -3);
        }

        public static string Ceaser(string lines, int key)
        {
            if (string.IsNullOrEmpty(lines))
                return lines;

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
