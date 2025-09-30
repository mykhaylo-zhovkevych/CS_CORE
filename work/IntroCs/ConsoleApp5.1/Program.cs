using System;

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

            // correct avarage from even numbers
            agg.Calculate(nums => (int)nums.Where(n => n % 2 == 0).Average());
           
            // odd / even max
            // number dividable per 7
            agg.Calculate(f => (int)f.Average());
            agg.Calculate(f => f.Min());
            agg.Calculate(f => f.Max());
        }

        private static void OnResultNewState(object? sender, EventArgs e)
        {
            if (sender is Aggregator agg)
            {
                Console.WriteLine("New Result: " + agg.Result);
            }
        }
    }
}
