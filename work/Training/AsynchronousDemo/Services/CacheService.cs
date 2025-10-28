using AsynchronousDemo.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDemo.Services
{
    public class CacheService
    {
        private readonly ConcurrentDictionary<string, DownloadResult> _cache = new();

        public bool TryGet(string url, out DownloadResult? result)
        {
            return _cache.TryGetValue(url, out result);
        }
        public void AddOrUpdate(DownloadResult result)
        {
            _cache[result.Url] = result;
        }

    }
}
