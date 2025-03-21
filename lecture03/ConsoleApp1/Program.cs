// See https://aka.ms/new-console-template for more information
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool canIVote = false;
            Console.WriteLine("Biggest Integer: " + int.MaxValue);
            Console.WriteLine("Smallest Integer: " + int.MinValue);

            Console.WriteLine("Biggest Long: " + long.MaxValue);
            Console.WriteLine("Smallest Long: " + long.MinValue);

            decimal decPiVal = 3.1415926535897932384626433832m;
            decimal decBigNum = 3.0000000000000000000000000001m;
            Console.WriteLine("DEC: PI + BigNum = " + (decPiVal + decBigNum));

            Console.WriteLine("Biggest Decimal: " + Decimal.MaxValue);

            Console.WriteLine();

            double dblPiVal = 3.14159265358979f;
            double dblBigNum = 3.00000000000002f;
            Console.WriteLine("DBL: PI + BigNum = " + (dblPiVal + dblBigNum));

            float fltPiVal = 3.14159265358979f;
            float fltBigNum = 3.00000000000002f;
            Console.WriteLine("FLT: PI + BigNum = " + (fltPiVal + fltBigNum));


            /* Other Data Types
            byte: 8-bit unsigned int 0 to 255
            char: 16-bit unicode character
            sbyte: 8-bit signed int -128 to 127
            short: 16-bit signed int -32,768 to 32,767
            uint: 32-bit unsigned int 0 to 4,294,967,295
            ulong: 64-bit unsigned int 0 to 18,446,744,073,709,551,615
            ushort: 16-bit unsigned int 0 to 65,535
            */

            // Casting 

            // 1. Parsing: Konvertierung von String zu verschiedenen Datentypen

            // Konvertiert den String "true" in einen booleschen Wert (true/false)
            bool boolFromStr = bool.Parse("true");
            Console.WriteLine($"boolFromStr: {boolFromStr}");

            // Konvertiert den String "100" in einen Integer (Ganzzahl)
            int intFromStr = int.Parse("100");
            Console.WriteLine($"intFromStr: {intFromStr}");

            // Konvertiert den String "100.123" in einen Double (Fließkommazahl)
            double dblFromStr = double.Parse("100.123");
            Console.WriteLine($"dblFromStr: {dblFromStr}");

            // 2. Umwandlung von Zahl zu String

            // Wandelt eine Double-Zahl in einen String um
            string strVal = dblFromStr.ToString();
            Console.WriteLine("strVal: " + strVal);

            // GetType() gibt den Datentyp der Variablen zurück
            Console.WriteLine("Typ von strVal: " + strVal.GetType());

            // 3. Explizite Konvertierung (Type-Casting)

            // Double zu Integer: Nachkommastellen gehen verloren
            double dbNum = 12.345;
            Console.WriteLine($"Integer (explizites Casting): { (int)dbNum }");

            // 4. Implizite Konvertierung (automatisch)

            // Int zu Long: Kein Datenverlust, da long mehr Speicherplatz hat
            int intNum = 10; // 32-Bit-Ganzzahl
            long longNum = intNum; // 64-Bit-Ganzzahl
            Console.WriteLine($"longNum: {longNum}");

            // 5. Beispiel für ungültiges Casting (führt zu Fehlern bei unsicherem Casting)

            // Dies würde einen Fehler verursachen, wenn der String nicht gültig ist
            try
            {
                int invalidInt = int.Parse("abc"); // Löst eine FormatException aus
            }
            catch (FormatException e)
            {
                Console.WriteLine("Fehler bei der Umwandlung: " + e.Message);
            }

            // Formatting Output

            Console.WriteLine("Currency : {0:c}", 23.455);
            Console.WriteLine("Pad with 0s : {0:d4}", 23);
            Console.WriteLine("3 Decimal Places : {0:f3}", 23.45556);
            Console.WriteLine("Commmas: {0:n4}", 2300);
            Console.WriteLine("Percents: {0:p}", .23);

        }
    }
}