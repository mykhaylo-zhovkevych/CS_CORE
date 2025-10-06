using System;
using System.Runtime.CompilerServices;

namespace ConsoleApp5._1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Aggregator agg = new Aggregator();

            //agg.ResultNewState += (s, e) =>
            //{
            //    Console.WriteLine("New Result: " + agg.Result);
            //};
            agg.ResultNewState += OnResultNewState;

            Random rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                agg.AddNumber(rnd.Next(1, 51));
            }

            agg.Calculate(EvenAverage);
            agg.Calculate(OddAverage);
            agg.Calculate(EvenMax);
            agg.Calculate(OddMax);
            agg.Calculate(DivisibleBy7Count);

            agg.Calculate(f => f.Min());
            agg.Calculate(f => f.Max());
        }

        static int EvenAverage(IEnumerable<int> nums)
        {
            return (int)nums.Where(n => n % 2 == 0).Average();

        }

        static int OddAverage(IEnumerable<int> nums)
        {
            return (int)nums.Where(n => n % 2 != 0).Average();
        }

        static int EvenMax(IEnumerable<int> nums)
        {
            return nums.Where(n => n % 2 == 0).Max();
        }

        static int OddMax(IEnumerable<int> nums)
        {
            return nums.Where(n => n % 2 != 0).Max();
        }

        static int DivisibleBy7Count(IEnumerable<int> nums)
        {
            return nums.Count(n => n % 7 == 0);
        }

        private static void OnResultNewState(object? sender, int result) => Console.WriteLine("New Result: " + result);
            
    }
}
