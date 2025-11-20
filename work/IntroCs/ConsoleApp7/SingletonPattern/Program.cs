using Microsoft.VisualBasic;
using SingletonPattern;
using StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            var settings = AppSettings.GetInstance();
            settings.Set("app_name", "Design Patterns Mastery");
            settings.Set("app_creator", "Danny");

            Console.WriteLine(settings.Get("app_name"));

            Test.Run();            
        }
    }
}
