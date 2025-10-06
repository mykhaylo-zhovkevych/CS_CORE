using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.HelperClasses
{
    public class ItemEventArgs : EventArgs
    {
        public string Message { get; set; }
        public Item Item { get; }
        public User? ReservedUser { get; set; }

        public ItemEventArgs(string message, Item item, User? reservedUser = null)
        {
            Message = message;
            Item = item;
            ReservedUser = reservedUser;
        }
    }
}
