using ConsoleApp4._3.Interfaces;

namespace ConsoleApp4._3.Items
{
    public class Key : Item, IUsable
    { 
        public Key() : base(name: "Key")  
        {
            Guid Id = Guid.NewGuid();

        }
        public void Use(Player player)
        {
            outputService.WriteLine("This is a Key. You can use it to unlock a door.");
        }
    }
}