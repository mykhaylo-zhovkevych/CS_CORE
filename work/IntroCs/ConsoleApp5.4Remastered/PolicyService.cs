using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp5._4Remastered
{
    public class PolicyService
    {

        public Dictionary<(User, Item), Policy> Policies = new Dictionary<(User, Item), Policy>();

        // Can cause side effects
        public PolicyService()
        {
            Policies[(User.Student, Item2.Book)] new Policy("Student Book Policy", User.Student, Item2.Book, 2, 0, 21);
        }

        public bool AddPolicy(Policy policy)
        {
            var key = (policy.User, policy.Item);
            if (Policies.ContainsKey(key)) return false;
            Policies[key] = policy;
            return true;
        }

        public bool UpdatePolicyValues(UserType userType, ItemType itemType,
                                          int extensions, decimal loanFees, int loanPeriodDays)
        {
            var key = (userType, itemType);
            if (!Policies.ContainsKey(key)) return false;
            policy.SetValues(extensions, loanFees, loanPeriodDays);
            return true;
        }

        public bool Remove(UserType userType, ItemType itemType)
        {
            return _policies.Remove((userType, itemType));
        }
    }
}