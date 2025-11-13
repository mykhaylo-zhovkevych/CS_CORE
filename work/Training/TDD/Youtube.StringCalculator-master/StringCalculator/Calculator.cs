using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {

        // First Cycle
        //public int Add(string numbers)
        //{
        //    var splitNumbers = numbers.Split(',');

        //    if (!splitNumbers.Any())
        //    {
        //        return 0;
        //    }

        //    if (splitNumbers.Length == 1)
        //    {
        //        if (splitNumbers[0].Length == 0)
        //        {
        //            return 0;
        //        }

        //        return int.Parse(splitNumbers[0]);
        //    }

        //    return int.Parse(splitNumbers[0]) + int.Parse(splitNumbers[1]);

        //}

        // Secound Cycle

        public int Add(string numbers)
        {
            var splitNumbers = numbers
                .Split(new []{','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);


            return splitNumbers.Sum();

        }

    }
}