using ConsoleApp5._4;
using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Users
{
    public class Admin : User
    {
        public override decimal LoanFees { get; }
        public override int Extensions { get; }

        public Admin(Guid id, string name) : base(id, name) 
        {
            LoanFees = 0.0m;
            Extensions = 0;
        }
    }
}