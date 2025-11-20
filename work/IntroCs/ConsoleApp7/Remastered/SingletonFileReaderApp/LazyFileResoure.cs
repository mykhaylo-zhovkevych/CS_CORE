using System.Runtime.CompilerServices;

namespace SingletonLoggerApp
{
    // No Concurrency Issue
    public sealed class LazyFileResource
    {

        private LazyFileResource() { }

        private static readonly Lazy<LazyFileResource> lazy = new Lazy<LazyFileResource>(() => new LazyFileResource()); 

        public static LazyFileResource Instance { get { return lazy.Value; } }  

    }
}