using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Solution
{
    public class CompressionDecorator : DataDecorator
    {
        public CompressionDecorator(IData iDate) : base(iDate)
        {

        }

        public override void Save(string data)
        {
            Console.WriteLine("Compressing data before saving.");
            var compressed = Compress(data);
            base._iData.Save(compressed);
        }

        // Simulation of compressing the Data
        public string Compress(string data)
        {
            return data.Substring(0, 9);
        }
    }
}
