using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Users
{
    internal class Teacher : User
    {
        public Teacher(Guid id, string name) : base(id, name) { }

        public override double PriceFactor => 0.8;
    }
}
