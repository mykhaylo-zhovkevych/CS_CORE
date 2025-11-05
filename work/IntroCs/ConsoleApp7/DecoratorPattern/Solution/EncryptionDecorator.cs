using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Solution
{
    public class EncryptionDecorator : DataDecorator
    {

        public EncryptionDecorator(IData iData) : base(iData)
        {
        }

        public override void Save(string data)
        {
            var encrypted = Encrypt(data);
            base._iData.Save(encrypted);
        }

        public string Encrypt(string data)
        {
            return "D*g23WE`F*";
        }



    }
}
