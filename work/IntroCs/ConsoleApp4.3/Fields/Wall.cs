using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    internal class Wall : Field
    {
        public Wall(string name) : base(name) {}

        public override bool CanEnter(Player player) => false;
    }
}
