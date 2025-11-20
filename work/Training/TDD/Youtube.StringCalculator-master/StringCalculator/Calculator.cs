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

            var delimiters = new List<char>{ ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                var splitOnFirstNewLine = numbers.Split(new[] { '\n' }, 2);
                var customDelimiter = splitOnFirstNewLine[0].Replace("//", string.Empty).Single();
                delimiters.Add(customDelimiter);
                numbers = splitOnFirstNewLine[1];
            }

            var splitNumbers = numbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();


            var negativeNumbers = splitNumbers.Where(x => x < 0).ToList();

            // 6. Refactoring 
            //var negativeNumbers = new List<int>();
            //foreach ( var potentiallyNegativeNumber in splitNumbers)
            //{
            //    if (potentiallyNegativeNumber < 0) 
            //    { 
                
            //        negativeNumbers.Add(potentiallyNegativeNumber);

            //    }
            //}

            if (negativeNumbers.Any())
            {
                throw new Exception($"Negatives not allowed: "+string.Join(",",negativeNumbers));
            }


            return splitNumbers.Sum();

        }

    }
}