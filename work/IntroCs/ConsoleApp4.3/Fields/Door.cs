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
        public override char Symbol => '╬';
        public Door(string name, (int, int) target) : base(name) => DoorTarget = target;

        public override bool CanEnter => true;
        
        public override void OnEnter(Player player) 
        {
            if (IsLocked)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    Console.WriteLine("Door is closed.");
                    CanEnter = false;
                    return;
                }
                IsLocked = false;
                player.Inventory.Remove(key);
                Console.WriteLine($"{player.Name} has opened the door");
            }

            // teleport after unlocking
            player.Position = DoorTarget;
            Console.WriteLine($"{player.Name} was teleported to {DoorTarget}");
        }
    }
}
