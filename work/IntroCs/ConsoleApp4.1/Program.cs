namespace ConsoleApp4._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CellarStorage<string> cellar01 = new CellarStorage<string>(4);
            CellarStorage<int> cellar02 = new CellarStorage<int>();

            cellar01.PushItemToStorage("string01");
            cellar01.PushItemToStorage("string02");
            cellar01.PushItemToStorage("string03");
            cellar02.PushItemToStorage(12);
            cellar02.PushItemToStorage(132);
            cellar01.PushItemToStorage("string04");

            cellar01.RemoveItemFromStorage();
            cellar02.RemoveItemFromStorage();

            cellar02.PrintAll();
            cellar01.PrintAll();

            Console.WriteLine(cellar02.IsFull());
            Console.WriteLine(cellar02.IsEmpty());

        }
    }
}
