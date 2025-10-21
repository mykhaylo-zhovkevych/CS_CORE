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
        public string Content { get; set; }
        public int Size { get; }

        public Order (string content)
        {
            OrderId = Guid.NewGuid();
            Content = content;
            Size = content.Length;
        }





    }
}
