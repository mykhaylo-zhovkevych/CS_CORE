using ConsoleApp5._4Remastered.Enum;
using System.Security.Cryptography.X509Certificates;

namespace LibraryAPI.Models
{
    public record CreateUserDto
    {
        public string Name { get; init; }
        public UserType UserType { get; init; }

        public CreateUserDto(string name, UserType userType)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }

            Name = name;
            UserType = userType;
        }
    }
}
