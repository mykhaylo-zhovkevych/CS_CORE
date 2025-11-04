using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern.BadSolution
{
    public class Test
    {

        public static void Run()
        {
            var settings = new AppSettings();
            Console.WriteLine(settings.Get("app_name"));
        }

    }
}
