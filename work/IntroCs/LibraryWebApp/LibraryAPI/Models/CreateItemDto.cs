using ConsoleApp5._4Remastered.Enum;

namespace LibraryAPI.Models
{
    public record CreateItemDto
    {
        public string Name { get; init; }
        public ItemType ItemType { get; init; }

        public CreateItemDto(string name, ItemType itemType)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Item name cannot be null or empty", nameof(name));
            }

            Name = name;
            ItemType = itemType;
        }
    }
}
