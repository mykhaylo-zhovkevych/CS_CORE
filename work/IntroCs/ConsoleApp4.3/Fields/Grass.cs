using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.Fields
{
    internal class Grass : Field
    {

        public override char Symbol => '.';
        public Grass(string name) : base(name) { }
        public override bool CanEnter => true;

        

    }
}
