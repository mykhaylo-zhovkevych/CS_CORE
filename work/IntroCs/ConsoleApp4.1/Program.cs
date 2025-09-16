namespace ConsoleApp4._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StackStorage<int> cellar = new StackStorage<int>(5);

            cellar.Push(2);
            cellar.Push(3);
            cellar.Pop();
            cellar.Push(3);
            cellar.Push(123);
            cellar.Pop();

            Console.WriteLine(cellar.ToString());


        }
    }
}
