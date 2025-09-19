
using ConsoleApp4._3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Items
{
    internal class Food : Item, IConsumable
    {
        public Food()
        {
            Guid Id = Guid.NewGuid();
            Name = "Food";
        }

        public void Consume(Player player)
        {
            player.Energy += 5;
            Console.WriteLine($"{player.Name} are a food and gained 5 energy");
        }
    }
}
