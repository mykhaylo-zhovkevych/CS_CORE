namespace ConsoleApp5._2
{ 

    public static class StringInvertor
    {
        public static string ReverseM(this string input)
        {
            return new string(input.Reverse().ToArray());
        }
    }
}