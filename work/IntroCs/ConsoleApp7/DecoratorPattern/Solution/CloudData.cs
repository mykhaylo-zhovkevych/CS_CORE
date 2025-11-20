using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Concrete Component
namespace DecoratorPattern.Solution
{
    public class CloudData : IData
    {
        private string _url;

        public CloudData(string url )
        {
            _url = url;
        }    
            
        public void Save(string data)
        {
            System.Console.WriteLine($"Saving data to {_url}: {data}");
        }
    }
}
