using AsynchronousDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDemo.Services
{
    public class Processor
    {

        public Task<Dictionary<string, int>> CountWordsAsync(DownloadResult result, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var words = result.Content
                    .Split(new[] { ' ', '\n', '\r', '\t', '.', ',', ';', ':', '!', '?' }, System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(w => w.Trim().ToLowerInvariant());

                var dict = words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
                return dict;
            }, cancellationToken);
        }

    }
}
