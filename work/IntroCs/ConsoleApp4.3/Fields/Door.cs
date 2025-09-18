using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    internal class Door : Field
    {
        public bool IsLocked { get; set; } = true;
        public (int x, int y) DoorTarget { get; }
        public Door(string name, (int, int) target) : base(name) => DoorTarget = target;

        public override bool CanEnter(Player player)
        {
            if(IsLocked)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    Console.WriteLine("Door is closed.");
                    return false;
                }
                IsLocked = false;
                player.Inventory.Remove(key);
                Console.WriteLine($"{player.Name} has opened a door ");
            }
            return true;
        }
        
        public override void OnEnter(Player player) 
        {

            player.Position = DoorTarget;
            Console.WriteLine($"{player.Name}was to {DoorTarget} teleported");
        }
    }
}
