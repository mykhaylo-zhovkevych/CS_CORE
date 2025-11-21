using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Interfaces
{
    public interface IUserRepository
    {

        User? GetExistingUserById(Guid id);

        User? GetExistingUser(string name, UserType userType);

        User UpdateUserProfile(Guid id, UserType newType);

    }
}
