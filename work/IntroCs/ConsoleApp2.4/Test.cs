using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class Test
    {
        public string TestName { get; set; }
        public int Id { get; set; }
        public bool IsTestFinished { get; set; }
        public string TestResult { get; set; }

        public Test(string testName, int id)
        {
            TestName = testName;
            Id = id;
            IsTestFinished = false;
            TestResult = string.Empty;
        }

        public void ResetTest()
        {
            IsTestFinished = false;
            TestResult = string.Empty;
            Console.WriteLine($"Test '{TestName}' was set back");
        }

        public override string ToString()
        {
            string status = IsTestFinished ? "Ended" : "Open";
            return $"Test: {TestName} (ID: {Id}) - Status: {status}";
        }
    }
}