using ConsoleApp4._3.Interfaces;
using ConsoleApp4._3.OutputServices;

namespace ConsoleApp4._3
{
    public abstract class Item
    {
        public Guid Id { get; }
        public string Name { get; set; }

        protected readonly IOutputService outputService;

        protected Item( string name, IOutputService? output = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            outputService = output ?? new StringBuilderOutputService();
        }

    }
}