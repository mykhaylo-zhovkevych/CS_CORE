using Microsoft.Win32;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp5._4Remastered
{
    public class PolicyService
    {

        public Dictionary<(User, Item), Policy> Policies = new Dictionary<(User, Item), Policy>(); 

        public PolicyService()
        {

        }

        public void AddPolicy(Policy policy)
        {
            var key = (policy.User, policy.Item);
            Policies[key] = policy;
        }

        public void RemovePolicy(User user, Item item)
        {
            
        }

    }
}