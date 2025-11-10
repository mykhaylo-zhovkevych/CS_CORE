using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public interface IProduce
    {
        // asnyc Method in interface?
        private async Task ProccessOrderAsync(Order order);

    }
}
