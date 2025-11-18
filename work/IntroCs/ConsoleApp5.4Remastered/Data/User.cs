using ConsoleApp5._4Remastered.Enum;
using System;
using System.Text.Json.Serialization;

namespace ConsoleApp5._4Remastered.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }

        [JsonConstructor]
        public User(string name, UserType userType)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserType = userType;
        }
    }
}