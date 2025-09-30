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

        public int Result { get; private set; }

        public event EventHandler? ResultNewState;

        public void AddNumber(int i) => _numbers.Add(i);

        public int Calculate(AggregationFunction newFunction)
        {
            if (newFunction == null) throw new ArgumentNullException(nameof(newFunction));

            // send result to subscriber
            ResultNewState?.Invoke(this, EventArgs.Empty);

            return newFunction(_numbers);;

        }
    }
}
