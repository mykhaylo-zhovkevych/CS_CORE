
namespace ConsoleApp2._3._1
{
    public class NotEnoughProductException : Exception
    {
        public NotEnoughProductException(string? message) : base(message)
        {
        }
    }
}