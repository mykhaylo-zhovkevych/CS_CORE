using StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public class Test
    {
        public static void Run()
        {
            // This will work beacuse ther is a global instance of the object
            var settings = AppSettings.GetInstance();
            Console.WriteLine(settings.Get("app_name"));
        }
    }
}
