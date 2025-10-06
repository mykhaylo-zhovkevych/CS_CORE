using ConsoleApp5._2;
using ConsoleApp5._2.Objects;
using ConsoleApp5._2.Users;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4
{
    public class DefaultBorrowPolicyProvider : IBorrowPolicyProvider
    {
        // Direct initialization: good for simple, constant initialization
        private readonly Dictionary<(Type userType, Type itemType), BorrowPolicy> _map = new();


        public DefaultBorrowPolicyProvider()
        {
            AddRule(typeof(Student), typeof(Book), new BorrowPolicy(30, 0.00m, 2));
            AddRule(typeof(Student), typeof(Magazine), new BorrowPolicy(30, 0.00m, 1));
            AddRule(typeof(Student), typeof(BoardGame), new BorrowPolicy(21, 0.00m, 1));
            AddRule(typeof(Student), typeof(VideoGame), new BorrowPolicy(21, 0.00m, 1));

            AddRule(typeof(Teacher), typeof(Book), new BorrowPolicy(30, 50.00m, 2));
            AddRule(typeof(Teacher), typeof(Magazine), new BorrowPolicy(30, 50.00m, 2));
            AddRule(typeof(Teacher), typeof(BoardGame), new BorrowPolicy(14, 50.00m, 2));
            AddRule(typeof(Teacher), typeof(VideoGame), new BorrowPolicy(14, 50.00m, 2));

            AddRule(typeof(ExternalUser), typeof(Book), new BorrowPolicy(30, 100.00m, 0));
            AddRule(typeof(ExternalUser), typeof(Magazine), new BorrowPolicy(30, 100.00m, 0));
            AddRule(typeof(ExternalUser), typeof(BoardGame), new BorrowPolicy(14, 100.00m, 0));
            AddRule(typeof(ExternalUser), typeof(VideoGame), new BorrowPolicy(14, 100.00m, 0));
        }

        public void AddRule(Type userType, Type itemType, BorrowPolicy policy)
        {
            // if types null throw error
            _map[(userType, itemType)] = policy;
        }

        // Is used as help method and obtains the default policy
        public BorrowPolicy GetPolicy(User user, Item item)
        {
            // ifs checks

            var policy = _map
                .Where(f => f.Key.userType.IsAssignableFrom(user.GetType()) &&
                            f.Key.itemType.IsAssignableFrom(item.GetType()))
                .Select(f => f.Value)
                .FirstOrDefault();

            // not clear in the specification if default is needed, otherwise can throw a exception
            return policy ?? throw new NonExistingPolicyException();


        }

    }
}
