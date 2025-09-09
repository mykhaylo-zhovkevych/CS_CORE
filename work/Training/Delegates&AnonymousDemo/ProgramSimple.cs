using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_AnonymousDemo
{
    public delegate int Operation(int x, int y);

    public class Calculator
    {
        public int Add(int x, int y) => x + y;
        public int Subtract(int x, int y) => x - y;
        public int Multiply(int x, int y) => x * y;
        public int Divide(int x, int y) => x / y;
    }

    public class ProgramSimple
    {
        public static int ExecuteOperation(int a, int b, Operation operation)
        {
            return operation(a, b);
        }

        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            
            Console.WriteLine("Add: " + ExecuteOperation(19, 5, calc.Add));
            Console.WriteLine("Multiply: " + ExecuteOperation(19, 5, calc.Multiply));
            Console.WriteLine("Subtract: " + ExecuteOperation(19, 5, calc.Subtract));
            Console.WriteLine("Divide: " + ExecuteOperation(19, 5, calc.Divide));


            Console.WriteLine("\nUsing lambda expressions:");
            Console.WriteLine("Add: " + ExecuteOperation(19, 5, (x, y) => x + y));
            Console.WriteLine("Multiply: " + ExecuteOperation(19, 5, (x, y) => x * y));
            Console.WriteLine("Subtract: " + ExecuteOperation(19, 5, (x, y) => x - y));
            Console.WriteLine("Divide: " + ExecuteOperation(19, 5, (x, y) => x / y));
        }
    }
}