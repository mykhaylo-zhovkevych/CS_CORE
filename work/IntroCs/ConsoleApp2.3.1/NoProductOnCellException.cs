
namespace ConsoleApp2._3._1
{
    [Serializable]
    public class NoProductOnCellException : Exception
    {
        public NoProductOnCellException()
        {
        }

        public NoProductOnCellException(string? message) : base(message)
        {
        }

        public NoProductOnCellException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}