using ConsoleApp4._3.Interfaces;

namespace ConsoleApp4._3.Items
{
    public class Bag : Item, IUsable
    { 
        public Bag() : base() 
        {
            Guid Id = Guid.NewGuid();
            Name = "Box"; 
        }
        public void Use(Player player)
        {
            outputService.WriteLine("Your bag has: " + string
                .Join(", ", player.Inventory
                .Select(i => i.Name)));

        }
    }    
}