using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Users
{
    public class Student : User
    {
        public Student(Guid id, string name) : base(id, name)
        {
            LoanFees = 0.0m;
            Extensions = 1;
        }

        public override int LoanPeriod { get; set; }
    }
}