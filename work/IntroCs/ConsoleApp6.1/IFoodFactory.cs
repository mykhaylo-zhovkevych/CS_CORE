using ConsoleApp6._1.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public interface IFoodFactory
    {
        Task<IFoodItem> ProduceAsync(ITaskExecutor worker); 
    }
}
