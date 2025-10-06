using ConsoleApp4._3.Interfaces;
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
        public override char Symbol => '╬';

        public Door(string name) : base(name) { }

        public override bool CanEnter { get; set; }

        public override bool MovePlayerToField(Player player)
        {
            if (!CanEnter)
            {
                var key = player.Inventory.OfType<Key>().FirstOrDefault();
                if (key == null)
                {
                    outputService.WriteLine("Door is closed.");
                    return CanEnter;
                }

                player.Inventory.Remove(key);
                outputService.WriteLine($"{player.Name} has opened the door");
                CanEnter = true;
            }

            outputService.WriteLine($"{player.Name} was went through {Name} normally");

            return CanEnter;
        }
    }
}
