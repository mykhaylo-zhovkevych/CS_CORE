using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Users
{
    public class Teacher : User
    {
        public Teacher(Guid id, string name) : base(id, name)
        {
            LoanFees = 50.0m;
            Extensions = 2;
        }

        public override int LoanPeriod { get; set; }

    }
}
