namespace ConsoleApp4._3.Items
{
    internal class Key : Item, IUsable
    { 
        public Key() 
        {
            Guid Id = Guid.NewGuid();
            Name = "key"; 

        }
        public void Use(Player player)
        {
            Console.WriteLine("This is a Key. You can use it to unlock a door.");
        }
    }
}