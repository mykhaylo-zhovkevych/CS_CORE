namespace ConsoleApp5._1
{
    internal class Program
    {

        public delegate int AggregationFunction(IEnumerable<int> value);

        public class Aggregator
        {
            private readonly List<int> _numbers = new List<int>();
            private AggregationFunction _function;
            public event EventHandler ResultNewState;

            public int Result { get; private set; }

            public void AddNumber (int i)
            {
                _numbers.Add(i);
            }

            public void SetFunction(AggregationFunction f)
            {
                _function = f;
                CalcNew();
            }

            private void CalcNew()
            {
                if(_function == null || _numbers.Count == 0)
                {
                    Result = 0;
                }

                else
                {
                    // Methode 
                    Result = _function(_numbers);
                }

                ResultNewState?.Invoke(this, EventArgs.Empty);
            }
        }
        

        static void Main(string[] args)
        {
            Aggregator agg = new Aggregator();

            agg.ResultNewState += (s, e) =>
            {
                Console.WriteLine("New Result: " + agg.Result);
            };
            agg.AddNumber(3);
            agg.AddNumber(5);
            agg.AddNumber(15);

            // SUM 
            agg.SetFunction(numbers => 
            {
                int sum = 0;
                foreach (var x in numbers) sum += x;
                return sum;
            });

            agg.AddNumber(10);

            // MAX
            agg.SetFunction(delegate (IEnumerable<int> numbers)
            {
                int max = int.MinValue;
                foreach (var x in numbers)
                    if (x > max) max = x;
                return max;
            });
        }
    }
}
