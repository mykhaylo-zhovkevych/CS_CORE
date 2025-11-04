using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Base Decorator
namespace DecoratorPattern.Solution
{
    public abstract class DataDecorator : IData
    {
        protected IData _iData;

        public DataDecorator(IData iData)
        {
            _iData = iData;

        }

        public abstract void Save(string data);
    }
}
