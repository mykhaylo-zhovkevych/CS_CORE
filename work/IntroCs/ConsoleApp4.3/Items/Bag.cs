namespace ConsoleApp4._3.Items
{
    internal class Bag : Item 
    { 
        public Bag() 
        {
            Guid Id = Guid.NewGuid();
            Name = "Box"; 
        }
        public override void Use(Player player)
        {
            Console.WriteLine("Inventory: " + string
                .Join(", ", player.Inventory
                .Select(i => i.Name)));

        }
    }    
}