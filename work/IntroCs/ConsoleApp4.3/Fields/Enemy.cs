using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    internal class Enemy : Field
    {
        public override char Symbol => '☠';
        public Enemy(string name) : base(name) { }

        public override bool CanEnter => true;

        public override bool MovePlayerToField(Player player)
        {
            var sword = player.Inventory.OfType<Sword>().FirstOrDefault();

            if (sword != null)
            {
                player.Inventory.Remove(sword);
                Console.WriteLine("You have protected yourslef with sword");
            }
            else
            {
                player.Energy -= 10;
                Console.WriteLine($"{player.Name} dont have sword, demage is -10!");
            }
            return CanEnter;
        }
    }
}
