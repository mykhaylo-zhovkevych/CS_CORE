namespace ConsoleApp4._3
{
    public abstract class Item
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }

    }
}