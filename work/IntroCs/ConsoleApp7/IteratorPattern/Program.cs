namespace IteratorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ShoppingList list = new ShoppingList();
            list.Push("Apple");
            list.Push("Banana");
            list.Push("Orange");

            var iterator = list.CreateIterator();

            while(iterator.HasNext())
            {
                Console.WriteLine(iterator.Current());
                iterator.Next();
            }


        }
    }
}
