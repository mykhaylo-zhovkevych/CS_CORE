using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    public class Wall : Field
    {

        public override char Symbol => '█';
        public Wall(string name) : base(name) {}

        public override bool CanEnter => false;

        public override bool MovePlayerToField(Player player)
        {
            outputService.WriteLine($"Is a Wall,{player.Name} cannot go further");
            return CanEnter;
        }
    }
}
