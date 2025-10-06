
using ConsoleApp4._3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Items
{
    public class Food : Item, IConsumable
    {
        public Food() : base(name: "Food")
        {
            Guid Id = Guid.NewGuid();
        }

        public void Consume(Player player)
        {
            player.Energy += 5;
            outputService.WriteLine($"{player.Name} are a food and gained 5 energy");
        }
    }
}
