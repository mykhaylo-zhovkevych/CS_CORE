using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverWeatherApp
{
    public class UsersPool
    {

        public List<User> Users { get; private set; } = new List<User>();


        public void AddUser(User user, BroadcastChannel channel)
        {
            Users.Add(user);
            channel.MessageBroadcasted += user.OnMessageReceived;
        }

        public void RemoveUser(User user, BroadcastChannel channel)
        {
            Users.Remove(user);
            channel.MessageBroadcasted -= user.OnMessageReceived;
        }
    }
}
