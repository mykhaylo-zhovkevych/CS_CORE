using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    public interface IOrderExecutor<in T>
    {
        void PreProcess(T order);
        void ExecuteOrder();
    }
}
