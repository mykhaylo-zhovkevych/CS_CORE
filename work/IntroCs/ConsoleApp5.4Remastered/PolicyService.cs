using ConsoleApp5._4Remastered.Exceptions;
using ConsoleApp5._4Remastered.Storage;
using ConsoleApp5._4Remastered.Enum;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp5._4Remastered
{
    public class PolicyService
    {
        public static Dictionary<(UserType UserType, ItemType ItemType), Policy> Policies { get; private set; } = new();

        public static bool AddPolicy(UserType userType, ItemType itemType, Policy policy)
        {
            var key = (userType, itemType);
            if (Policies.ContainsKey(key))
            {
                return false;
            }

            Policies.Add(key, policy);
            return true;
        }

        public static Policy UpdatePolicyValues(UserType userType, ItemType itemType,
                                          uint extensions, decimal loanFees, uint loanPeriodDays)
        {
            var key = (userType, itemType);
            if (!Policies.ContainsKey(key))
            {
                throw new NonExistingPolicyException();
            }
            Policies[key].SetValues(extensions, loanFees, loanPeriodDays);
            return Policies[key];
        }

        public static bool Remove(UserType userType, ItemType itemType)
        {
            var key = (userType, itemType);
            return Policies.Remove(key);
        }

        public static Policy GetPolicy(UserType userType, ItemType itemType)
        {
            var key = (userType, itemType);
            if (!Policies.ContainsKey(key))
            {
                throw new NonExistingPolicyException();
            }
            return Policies[key];
        }

        public static void ClearPolicies()
        {
            PolicyService.Policies.Clear();
        }
    }
}