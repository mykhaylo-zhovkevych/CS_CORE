using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverWeatherApp
{
    /// <summary>
    /// Represents a User who can subscribe to weather updates
    /// Has method that reaches for event(publisher)
    /// </summary>
    public class User
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }


        public User(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }


        public void OnMessageReceived(object? sender, string message)
        {
            Console.WriteLine($"User {Name} received weather update: {message}");
        }

    }
}
