using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverWeatherApp
{
    /// <summary>
    /// Send to all observers the weather update
    /// 
    /// </summary>
    public class BroadcastChannel
    {
        // API Data
        private string _currentMessage = "Sunny, 25°C";
        // API Refreshment
        private int _refreshIntervalInSeconds = 5;
        private CancellationToken? _cts;

        public event EventHandler<string> MessageBroadcasted;



        public async Task SendMessageAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _currentMessage = FetchWeather();

                MessageBroadcasted?.Invoke(this, _currentMessage);
                await Task.Delay(TimeSpan.FromSeconds(_refreshIntervalInSeconds) , token);

            }
        }

        private string FetchWeather()
        {
            var now = DateTime.Now;
            return $"Weather update at {now}: Sunny, 25°C";
        }

    }
}
