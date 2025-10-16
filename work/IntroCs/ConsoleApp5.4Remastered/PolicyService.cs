using ConsoleApp5._4Remastered.Data;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp5._4Remastered
{
    public class PolicyService
    {
        
        /*
         The idea that i have a one user type 
         and it it check if that type is allwod or in policy existing like 
         one policy for one type of user and not secific each one

         like i can have a toused of user with same type and must 
         
         
         
         */



        public static Dictionary<(User user, Item item), Policy> Policies { get; private set; } = new();

        // Can cause side effects
        static PolicyService()
        {
            //var defaultUser = new User ("Default Student", Enum.UserType.Student);
            //var defaultItem = new Item ("Default Book", Enum.ItemType.BoardGame);

            //var defaultPolicy = new Policy { PolicyName = "Student Book Policy", User = defaultUser, Item = defaultItem };

            //defaultPolicy.SetValues(extensions: 2, loanFees: 0.5m, loanPeriod: 14);

            //AddPolicy(defaultPolicy);

        }

        public static bool AddPolicy(Policy policy)
        {
            var key = (policy.User, policy.Item);
            if (Policies.ContainsKey(key)) return false;
            Policies[key] = policy;
            return true;
        }

        public static bool UpdatePolicyValues(User user, Item item,
                                          int extensions, decimal loanFees, int loanPeriodDays)
        {
            var key = (user, item);
            if (!Policies.ContainsKey(key)) return false;
            Policies[key].SetValues(extensions, loanFees, loanPeriodDays);
            return true;
        }

        public static bool Remove(User user, Item item)
        {
            return Policies.Remove((user, item));
        }

        public static Policy GetPolicy(User user, Item item)
        {
            var key = (user, item);

            return Policies[key];
        }
    }
}