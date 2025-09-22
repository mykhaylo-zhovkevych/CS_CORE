using ConsoleApp4._3.Interfaces;

namespace ConsoleApp4._3.Items
{
    public class Sword : Item, IUsable
    {
        public Sword() 
        {
            Guid Id = Guid.NewGuid();
            Name = "Sword";
        }
        public void Use(Player player)
        {
            bool sure = true;
            Console.WriteLine("You can trage your Sword for one Food");
            
            int index = player.Inventory.IndexOf(this);


            if (sure)
            {
                player.Inventory[index] = new Food();
                Console.WriteLine("You traded a sword for one item of Food");
            }
            else
            {
                Console.WriteLine("You keep your Sword.");
            }

        }
    }
}