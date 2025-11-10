namespace SingletonLoggerApp
{
    public class Testprogram
    {
        static void function(string[] args)
        {
            LazyFileResource lazyFileResource = LazyFileResource.Instance;
            FileResource fileResource = FileResource.GetInstance();
        }
    }
}
