using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public abstract int LoanPeriod { get; set; }
        public abstract decimal LoanFees { get; set; }
        public abstract int Extensions { get; set; }

        public User(Guid id, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public virtual string ReportBorrowedItems()
        {
        
            return "";
        }
    }
}