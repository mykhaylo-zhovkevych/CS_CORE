using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2.Users
{
    internal class Student : User
    {
        public Student(Guid id, string name) : base(id, name) { }
        public override double PriceFactor => 1.3;
    }
}
