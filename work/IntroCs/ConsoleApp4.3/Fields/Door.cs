using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    public class Door : Field
    {
        public (int x, int y) DoorTarget { get; }
        public override char Symbol => '╬';
        public Door(string name, (int, int) target) : base(name) => DoorTarget = target;

        public override bool CanEnter => false;
        
        public override bool MovePlayerToField(Player player) 
        {
            if (!CanEnter)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    Console.WriteLine("Door is closed.");
                    return CanEnter;
                }
                
                player.Inventory.Remove(key);
                Console.WriteLine($"{player.Name} has opened the door");
                CanEnter = true;
            }

            // teleport after unlocking
            player.Position = DoorTarget;
            Console.WriteLine($"{player.Name} was teleported to {DoorTarget}");
            return CanEnter;
        }
    }
}
