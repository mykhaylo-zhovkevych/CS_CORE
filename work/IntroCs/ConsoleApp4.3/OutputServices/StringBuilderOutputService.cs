using ConsoleApp4._3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3.OutputServices
{
    public class StringBuilderOutputService : IOutputService
    {
        private readonly StringBuilder _sb = new StringBuilder();
        public void WriteLine(string message)
        {
            string processed = ProcessMessage(message);

            _sb.AppendLine(processed);

            Console.WriteLine(processed);
        }
        private string ProcessMessage(string input)
        {
            return input.ToUpper();
        }

    }
}
