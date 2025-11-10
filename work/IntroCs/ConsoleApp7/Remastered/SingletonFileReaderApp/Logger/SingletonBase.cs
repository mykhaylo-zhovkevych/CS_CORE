namespace SingletonLoggerApp.Logger
{ 
    public abstract class SingletonBase
    {
        public void Log(Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now}] ERROR: {ex.Message}");

        }
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

    }
}