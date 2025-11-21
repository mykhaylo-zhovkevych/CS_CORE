using ConsoleApp5._4Remastered.Enum;
using System.Text.Json.Serialization;
using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Interfaces;

namespace ConsoleApp5._4Remastered.Storage
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }


        public User(string name, UserType userType)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserType = userType;
        }

        //public void UpdateUserProfile(UserType selectedUserType)
        //{
        //    UserType = selectedUserType;
        //}
    }
}