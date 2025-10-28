using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousDemo.Services
{
    public static class RetryHelper
    {

        public static async Task<T> RetryOnExceptionAsync<T>(Func<Task<T>> action, int maxAttemps = 3, TimeSpan? initialDelay = null)
        {
            initialDelay ??= TimeSpan.FromMilliseconds(200);
            var delay = initialDelay.Value;

            // A for loop can run indefinitely like ;; 
            for (int attemp = 1; ; attemp++)
            {
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (Exception) when (attemp < maxAttemps)
                {
                    await Task.Delay(delay).ConfigureAwait(false);
                    delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
                }
            }
        }

    }
}
