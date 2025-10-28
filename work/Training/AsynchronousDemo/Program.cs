using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsynchronousDemo.Services;
using AsynchronousDemo.Models;

namespace AsynchronousDemo
{
    class Program
    {
        static async Task<int> Main()
        {

            var urls = new[]
            {
            "https://theuselessweb.com/",
            "https://www.google.com/",
            "https://httpbin.org/delay/1"
            };


            var cache = new CacheService();
            var downloader = new Downloader(cache);
            var processor = new Processor();


            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Cancel signal received - stop...");
                e.Cancel = true;
                cts.Cancel();
            };


            var progress = new Progress<(int done, int total)>((p) =>
            {
                Console.WriteLine($"Progress: {p.done}/{p.total}");
            });


            try
            {
                int done = 0;
                var total = urls.Length;


                foreach (var url in urls)
                {
                    cts.Token.ThrowIfCancellationRequested();


                    Console.WriteLine($"Starts Download: {url}");
                    var downloadResult = await downloader.GetContentAsync(url, cts.Token).ConfigureAwait(false);


                    if (downloadResult.Error != null)
                    {
                        Console.WriteLine($"Error During Downloading {url}: {downloadResult.Error.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"Download from {url} ended (FromCache: {downloadResult.FromCache})");

                        var wordCounts = await processor.CountWordsAsync(downloadResult, cts.Token).ConfigureAwait(false);
                        Console.WriteLine($"Words in Document: {wordCounts.Count}");
                    }


                    done++;
                    ((IProgress<(int, int)>)progress).Report((done, total));
                }


                Console.WriteLine("all URLs are processed");
                return 0;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Process broken.");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 2;
            }
        }
    }
}
