using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Data
{
    // tiny factory (switch or delegate) 
    // For creating specifiy user based on enum 

    // or create 

    public class Policy
    {

        public Guid Id { get; init; }
        public string? PolicyName { get; init }

        // I can change these properties if i will needed
        public Item Item { get; init }
        public User User { get; init }

        // TODO: think way of resetting the properties
        public int Extensions { get; private set; }
        public decimal LoanFees { get; private set; }
        public int LoanPeriod { get; private set; }


        public Policy(string policyName, Item item, User user, int extensions, decimal loanFees, int loanPeriod)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
            Item = item;
            User= user;
            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriod = loanPeriod;

        }

        internal void SetValues(int extensions, decimal loanFees, int loanPeriod)
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
