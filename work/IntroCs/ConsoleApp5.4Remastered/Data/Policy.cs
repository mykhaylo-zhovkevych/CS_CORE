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

        public Dictionary<Guid, Policy> Policies { get; private set; }

        public Guid Id { get; init; }
        public string? PolicyName { get; }

        // I can change these properties if i will needed
        public Item item { get; set; }
        public User user { get; set; }

        // TODO: think way of resetting the properties
        public int Extensions { get; set; }
        public decimal LoanFees { get; set; }
        public int LoanPeriod { get; set; }


        public Policy(string policyName, Item item, User user, int extensions, decimal loanFees, int loanPeriod)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
            this.item = item;
            this.user = user;
            Extensions = extensions;
            LoanFees = loanFees;
            LoanPeriod = loanPeriod;

            Policies = new Dictionary<Guid, Policy>
            {
                { (Guid.NewGuid()), new Policy("Policy 1 Monat", new item.Book, new user.Student, 2, 100.0m, 21),
                
                { (Guid.NewGuid()), new Policy("Policy 3 Wochen", new Magazine("Magazine"), new RegularUser("Regular"), 1, 0m, 21) },
                { (Guid.NewGuid()), new Policy("Policy 2 Wochen", new DVD("DVD"), new RegularUser("Regular"), 1, 1.5m, 14) },

            };

        }

        // Add new policy 
        // Delete old policy 
        // Update existing policy


    }
}
