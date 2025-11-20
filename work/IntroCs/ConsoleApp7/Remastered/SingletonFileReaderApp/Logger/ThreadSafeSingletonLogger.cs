namespace SingletonLoggerApp.Logger
{ 
    public class ThreadSafeSingletonLogger : SingletonBase
    {

        private static readonly object threadsafeLock = new object();

        private static ThreadSafeSingletonLogger _instance;

        private ThreadSafeSingletonLogger()
        {

        }

        // Double-checked locking pattern
        public static ThreadSafeSingletonLogger GetInstance()
        {
            if (_instance == null)
            {
                lock (threadsafeLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ThreadSafeSingletonLogger();
                    }
                }
            }
            return _instance;
        }
    }
}