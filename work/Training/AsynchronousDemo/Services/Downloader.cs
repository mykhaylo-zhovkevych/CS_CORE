using AsynchronousDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDemo.Services
{
    public class Downloader
    {
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private readonly CacheService _cache;

        public Downloader(CacheService cache)
        {
            _cache = cache;
        }

        public async Task<DownloadResult> GetContentAsync(string url, CancellationToken cancellationToken)
        {
            // First cache check
            if (_cache.TryGet(url, out var cached))
            {
                return cached with { FromCache = true };
            }
            // Retry logic through RetryHelper
            try
            {
                var content = await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    using var req = new HttpRequestMessage(HttpMethod.Get, url);
                    using var resp = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                    var s = await resp.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    return s;
                }).ConfigureAwait(false);

                var result = new DownloadResult(url, content, FromCache: false);
                _cache.AddOrUpdate(result);
                return result;
            } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                return new DownloadResult(url, string.Empty, FromCache: false, Error: ex);
            }
        }
    }
}
