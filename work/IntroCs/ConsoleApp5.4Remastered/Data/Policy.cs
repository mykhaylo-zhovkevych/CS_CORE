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

        //public ItemType ItemType { get; init; }
        //public UserType UserType { get; init; }

        public uint Extensions { get; private set; }
        public decimal LoanFees { get; private set; }
        // Confusion here with naming 
        public uint LoanPeriodInDays { get; private set; }


        public Policy()
        {
            Id = Guid.NewGuid();
        }

        public Policy(string policyName, uint extensions, decimal loanFees, uint loanPeriodInDays)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
            //ItemType = item;
            //UserType = user;
            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriodInDays = loanPeriodInDays;

        }

        // For testing reason: changed to public, must be internal per default 
        public void SetValues(uint extensions, decimal loanFees, uint loanPeriodInDays)
        {
            if (extensions < 0) throw new ArgumentOutOfRangeException(nameof(extensions));
            if (loanFees < 0) throw new ArgumentOutOfRangeException(nameof(loanFees));

            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriodInDays = loanPeriodInDays;
        }
    }
}