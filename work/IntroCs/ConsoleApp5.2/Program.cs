namespace ConsoleApp5._2
{
    internal class Program
    {

        public class Generator
        {

            private static readonly string DefaultAlphabet = new string(Enumerable.Range('a',26).Select(c => (char)c)
                                                                                                .Concat(Enumerable.Range('A',26)
                                                                                                .Select(c => (char)c)).ToArray()
            );


            public string GenerateString(int length, string? alphabet = null)
            {
                var chars = alphabet ?? DefaultAlphabet;
                var rnd = new Random();

                return new string(
                    Enumerable.Range(0, length)
                              .Select(_ => chars[rnd.Next(chars.Length)])
                              .ToArray());
            }
        }


        static void Main(string[] args)
        {
            var gen = new Generator();

            string random = gen.GenerateString(23);
            string randomCustom = gen.GenerateString(20, "ABC123");

            Console.WriteLine("Random: " + random);
            Console.WriteLine("Custom: " + randomCustom);

            string reversed = random.ReverseM();
            Console.WriteLine("Reversed: " + reversed);

        }
    }
}
