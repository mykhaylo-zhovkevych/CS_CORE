namespace ConsoleApp5._2
{ 

    public static class StringInvertor
    {
        public static string ReverseInput(this string input)
        {
            char[] reversedChars = new char[input.Length];

            for (int i = 1; i <= input.Length; i++)
            {
                reversedChars[i - 1] = input[input.Length - i];
            }

            return new string(reversedChars);
        }
    }
}