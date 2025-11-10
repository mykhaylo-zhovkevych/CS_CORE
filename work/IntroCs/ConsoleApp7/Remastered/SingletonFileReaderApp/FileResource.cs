using System;

namespace SingletonLoggerApp
{
    public sealed class FileResource
    {
        private FileResource()
        {

        }

        private static FileResource _instance = null;

        public static FileResource GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FileResource();
            }
            return _instance;
        }
    }
}