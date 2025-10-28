using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class GenericCustomer
    {
        public int CustomerOrderNumber { get; }

        public GenericCustomer(int customerOrderNumber)
        {
            CustomerOrderNumber = customerOrderNumber;
        }
    }
}