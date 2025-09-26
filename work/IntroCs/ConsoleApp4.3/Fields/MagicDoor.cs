using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    // Generalization
    public class MagicDoor : Door
    {
        public (int x, int y) DoorTarget { get; }
        public override char Symbol => '║';

        public MagicDoor(string name, (int, int) doorTarget) : base(name)
        {
            DoorTarget = doorTarget;
        }

        public override bool MovePlayerToField(Player player)
        {
            if (!CanEnter)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    outputService.WriteLine("Magic Door is closed.");
                    return CanEnter;
                }

                player.Inventory.Remove(key);
                outputService.WriteLine($"{player.Name} has opened the magic door");
                CanEnter = true;
            }
            player.Energy -= 5;
            return CanEnter;
        }
    }
}
