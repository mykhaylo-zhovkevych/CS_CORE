namespace ConsoleApp4._3
{
    internal abstract class Item
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }

    }
}