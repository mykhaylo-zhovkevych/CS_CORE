using ConsoleApp5._4Remastered.Exceptions;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp5._4Remastered
{
    public class PolicyService
    {
        public static Dictionary<(Enum.UserType UserType, Enum.ItemType ItemType), Policy> Policies { get; private set; } = new();


        public static bool AddPolicy(Policy policy)
        {
            var key = (policy.UserType, policy.ItemType);
            if (Policies.ContainsKey(key))
            {
                return false;
            }

            Policies.Add(key, policy);
            return true;
        }

        public static Policy UpdatePolicyValues(Enum.UserType userType, Enum.ItemType itemType,
                                          int extensions, decimal loanFees, int loanPeriodDays)
        {
            var key = (userType, itemType);
            if (!Policies.ContainsKey(key))
            {
                throw new NonExistingPolicyException();
            }
            Policies[key].SetValues(extensions, loanFees, loanPeriodDays);
            return Policies[key];
        }

        public static bool Remove(Policy policy)
        {
            var key = (policy.UserType, policy.ItemType);
            return Policies.Remove(key);
        }

        public static Policy GetPolicy(Enum.UserType userType, Enum.ItemType itemType)
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