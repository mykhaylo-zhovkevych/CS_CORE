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

        static PolicyService()
        {
            var defaultStudent = new User("Default Student", Enum.UserType.Student);
            var defaultTeacher = new User("Default Teacher", Enum.UserType.Teacher);

            
            var defaultBook = new Item("Default Book", Enum.ItemType.Book);
            var defaultBoardGame = new Item("Default BoardGame", Enum.ItemType.BoardGame);

            var Student_Book_Policy = new Policy { PolicyName = "Student_Book_Policy", User = defaultStudent, Item = defaultBook };
            var Student_BoardGame_Policy = new Policy { PolicyName = "Student_BoardGame_Policy", User = defaultStudent, Item = defaultBoardGame };

            Student_Book_Policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);
            Student_BoardGame_Policy.SetValues(extensions: 1, loanFees: 100.0m, loanPeriod: 21);

            AddPolicy(Student_Book_Policy);
            AddPolicy(Student_BoardGame_Policy);

        }

        public static bool AddPolicy(Policy policy)
        {
            var key = (policy.User.UserType, policy.Item.ItemType);
            if (Policies.ContainsKey(key)) return false;
            Policies[key] = policy;
            return true;
        }

        public static bool UpdatePolicyValues(User user, Item item,
                                          int extensions, decimal loanFees, int loanPeriodDays)
        {
            var key = (user.UserType, item.ItemType);
            if (!Policies.ContainsKey(key)) return false;
            Policies[key].SetValues(extensions, loanFees, loanPeriodDays);
            return true;
        }

        public static bool Remove(Policy policy)
        {
            var key = (policy.User.UserType, policy.Item.ItemType);
            return Policies.Remove(key);
        }
        // Is it okay to have a exceptions defined from the generic methods by themselves?
        public static Policy GetPolicy(User user, Item item)
        {
            var key = (user.UserType, item.ItemType);
            return Policies[key];
        }
    }
}