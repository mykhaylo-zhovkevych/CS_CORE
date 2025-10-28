using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Order 
    {
        public Guid OrderId { get; }
        public string OrderName { get; internal set; }
        public string Content { get; internal set; }
        public uint Size { get; set; }

        public Order (string orderName, string content, uint size)
        {
            OrderId = Guid.NewGuid();
            OrderName = orderName;
            Content = content;
            Size = size;
        }
    }
}
