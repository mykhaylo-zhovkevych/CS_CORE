using ConsoleApp4._3.Interfaces;
using ConsoleApp4._3.OutputServices;

namespace ConsoleApp4._3
{
    public abstract class Item
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }

        protected readonly IOutputService outputService;

        protected Item(IOutputService output = null)
        {
            outputService = output ?? new StringBuilderOutputService();
        }

    }
}