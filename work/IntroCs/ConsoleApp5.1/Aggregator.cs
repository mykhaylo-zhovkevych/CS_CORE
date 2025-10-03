using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp5._1.Program;

namespace ConsoleApp5._1
{
    public class Aggregator
    {
        public delegate int AggregationFunction(IEnumerable<int> value);
        private readonly List<int> _numbers = new List<int>();

        public event EventHandler<int> ResultNewState;

        public void Calculate(AggregationFunction newFunction)
        {
            int result = newFunction(_numbers);

            ResultNewState?.Invoke(this, result);

        }

        public void AddNumber(int i) => _numbers.Add(i);
    }
}
