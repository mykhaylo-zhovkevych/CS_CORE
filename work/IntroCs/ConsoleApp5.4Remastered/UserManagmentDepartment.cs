using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered
{
    public class UserManagmentDepartment
    {
        private Library _library;

        public UserManagmentDepartment(Library library) 
        {
            _library = library;
        }

        public User UpdateUserProfile(User user, UserType selectedUserType)
        {
            if (user.Id == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty", nameof(user.Id));
            }

            var foundEntries = _library.Users
                .Where(u => u.Id == user.Id)
                .ToList();

            if (!foundEntries.Any())
            {
                throw new InvalidOperationException($"Your User was not found");
            }

            var updatedUser = foundEntries.First();
            updatedUser.UserType = selectedUserType;

            return updatedUser;
        }
    }
}
