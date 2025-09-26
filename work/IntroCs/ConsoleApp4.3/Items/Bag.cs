using ConsoleApp4._3.Interfaces;

namespace ConsoleApp4._3.Items
{
    public class Bag : Item, IUsable
    { 
        public Bag() : base(name: "Box") 
        {
            Guid Id = Guid.NewGuid();
        }
        public void Use(Player player)
        {
            outputService.WriteLine("Your bag has: " + string
                .Join(", ", player.Inventory
                .Select(i => i.Name)));

        }
    }    
}