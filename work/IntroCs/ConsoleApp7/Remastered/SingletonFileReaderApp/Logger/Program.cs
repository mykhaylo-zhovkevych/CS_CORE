using SingletonLoggerApp;

namespace SingletonLoggerApp.Logger
{
    public class Programm
    {
        private readonly SingletonBase singletonOne = ThreadSafeSingletonLogger.GetInstance();
        private readonly SingletonBase singletonTwo = ThreadSafeSingletonLogger.GetInstance();

        static void Main(string[] args)
        {
            var program = new Programm();
            program.Run();
        }

        private void Run()
        {
            if (ReferenceEquals(singletonOne, singletonTwo))
            {
                Console.WriteLine("Both instances are the same.");
            }
            else
            {
                Console.WriteLine("Different instances.");
            }

            singletonOne.Log("This is a log message from singletonOne.");
        }
    }
}   