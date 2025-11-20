using SingletonLoggerApp;

namespace SingletonLoggerApp.Logger
{
    public class ClassicSingletonLogger : SingletonBase
    {
        private static ClassicSingletonLogger _instance;

        private ClassicSingletonLogger()
        {
        }


        public static ClassicSingletonLogger GetInstance()
        {
            if (_instance == null )
            {
                _instance = new ClassicSingletonLogger();
            }
            return _instance;
        }
    }
}