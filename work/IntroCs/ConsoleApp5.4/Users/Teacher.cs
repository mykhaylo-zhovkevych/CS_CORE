using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Users
{
    public class Teacher : User
    {
        public Teacher(Guid id, string name) : base(id, name) { }

        public override int LoanPeriod { get; set; }
        public override decimal LoanFees { get; set; }
        public override int Extensions { get; set; }
    }
}
