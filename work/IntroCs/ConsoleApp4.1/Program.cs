namespace ConsoleApp4._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StackStorage<int> cellar = new StackStorage<int>(5);

            cellar.PushStackStorage(2);
            cellar.PushStackStorage(3);

            cellar.RemoveStackStorage();

            cellar.PrintAll();

        }
    }
}
