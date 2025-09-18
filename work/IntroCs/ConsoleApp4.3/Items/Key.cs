namespace ConsoleApp4._3.Items
{
    internal class Key : Item 
    { 
        public Key() 
        {
            Guid Id = Guid.NewGuid();
            Name = "key"; 

        }
        public override void Use(Player player)
        {
            Console.WriteLine("This is a Key. You can use it to unlock a door.");
        }
    }
}