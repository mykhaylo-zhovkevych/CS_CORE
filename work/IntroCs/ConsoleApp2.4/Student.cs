using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace ConsoleApp2._4
{
    internal class Student : User
    {

        public Student(string name , int id) : base(name, id)
        {

        }

        public void TakeTest(Test test)
        {
            if (test == null) 
                throw new ArgumentNullException(nameof(test), "Test cannot be null");


        Random random = new Random();
        int score = random.Next(50, 101);
        bool passed = score >= 60;

        test.IsTestFinished = true;
        test.TestResult = $"Punkt Score: {score}/100 ({(passed ? "Pass" : "Did not Pass")})";

        Console.WriteLine($"Student {Name} has Test '{test.TestName}' finished.");
        Console.WriteLine($"Outcome: {test.TestResult}");

        }
    }
}
