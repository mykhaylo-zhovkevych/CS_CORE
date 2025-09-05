using System.Text;
using System.Diagnostics;

string s1 = "Hello";
string s2 = s1;

s1 += ", World!";
Console.WriteLine(s1);
Console.WriteLine(s2);

//StringBuilder sb = new();
//sb.Append("Hello");
//sb.Insert(5, "World ");
//sb.Replace("World", " C#");

//Console.WriteLine(sb.ToString());

// --- Performance Tetst ---

int iterations = 100000;
string text = "Hallo";

Stopwatch swString = Stopwatch.StartNew();

string resultString = text;
for (int i = 0; i < iterations; i++)
{
    resultString += "!";
}
swString.Stop();
Console.WriteLine("String-Länge " + resultString.Length);
Console.WriteLine("String-Dauer" + swString.ElapsedMilliseconds + "ms");


Stopwatch swSB = Stopwatch.StartNew();

StringBuilder sb = new StringBuilder();
for (int i = 0; i < iterations; i++)
{
    sb.Append("!");
}

swSB.Stop();
Console.WriteLine("StringBuilder-Länge " + sb.Length);
Console.WriteLine("StringBuilder-Dauer" + swSB.ElapsedMilliseconds + "ms");