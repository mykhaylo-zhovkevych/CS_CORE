using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Users
{
    public class ExternalUser : User
    {
        public override decimal LoanFees { get; }
        public override int Extensions { get; }

        public ExternalUser(string name) : base(name)
        {
            LoanFees = 100.0m;
            Extensions = 0;
        }
    }
}