using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp.SimpleExample
{
    public class ApplePayPayment : IPayment
    {

        public void Pay(double amount)
        {
            Console.WriteLine($"Successfully paid {amount} to merchant using a Apple Play.");
        }
    }
}
