using ConsoleApp5._4Remastered.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Data
{

    public class Policy
    {
        public Guid Id { get; init; }
        public string? PolicyName { get; init; }

        public ItemType ItemType { get; init; }
        public UserType UserType { get; init; }

        public int Extensions { get; private set; }
        public decimal LoanFees { get; private set; }
        public int LoanPeriod { get; private set; }


        public Policy()
        {
            Id = Guid.NewGuid();
        }

        public Policy(string policyName, ItemType item, UserType user, int extensions, decimal loanFees, int loanPeriod)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
            ItemType = item;
            UserType = user;
            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriod = loanPeriod;

        }

        // For testing reason: changed to public, must be internal per default 
        public void SetValues(int extensions, decimal loanFees, int loanPeriod)
        {
            if (extensions < 0) throw new ArgumentOutOfRangeException(nameof(extensions));
            if (loanFees < 0) throw new ArgumentOutOfRangeException(nameof(loanFees));
            if (loanPeriod <= 0) throw new ArgumentOutOfRangeException(nameof(loanPeriod));

            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriod = loanPeriod;
        }
    }
}