using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    public class FakeDoor : Door
    {
        public override char Symbol => '║';

        public FakeDoor(string name) : base(name, default) { }

        public override bool MovePlayerToField(Player player)
        {
            if (!CanEnter)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    outputService.WriteLine("FakeDoor is closed.");
                    return CanEnter;
                }

                player.Inventory.Remove(key);
                player.Energy -= 15;
                outputService.WriteLine($"{player.Name} has opened the fake door");
                outputService.WriteLine($"{player.Name} lost 15 energy for opening fake door");
                CanEnter = true;
            }

            outputService.WriteLine($"{player.Name} passed through {Name} normally.");
            return CanEnter;
        }
    }
}
