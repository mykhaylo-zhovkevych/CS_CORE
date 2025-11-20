using ConsoleApp5._4Remastered.Enum;

namespace LibraryAPI.Models
{
    public record ChangeProfileDto
    {
        public UserType UserType { get; init; }

        public ChangeProfileDto(UserType userType)
        {
            UserType = userType;
        }
    }
}
