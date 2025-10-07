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
        public ExternalUser(Guid id, string name) : base(id, name)
        {
            LoanFees = 100.0m;
            Extensions = 0;
        }

        public override int LoanPeriod { get; set; }

    }
}
