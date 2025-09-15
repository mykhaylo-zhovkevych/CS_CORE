using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    internal interface IOrderExecutor<in T>
    {

        void ExecuteOrder(T order);
    }
}
