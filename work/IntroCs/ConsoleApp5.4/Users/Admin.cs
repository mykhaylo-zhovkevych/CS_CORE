using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Users
{
    public class Admin : User
    {
        public Admin(Guid id, string name) : base(id, name) { }
    
        public override int LoanPeriod { get; set; }
        public override decimal LoanFees { get; set; }
        public override int Extensions { get; set; }

    }
}
