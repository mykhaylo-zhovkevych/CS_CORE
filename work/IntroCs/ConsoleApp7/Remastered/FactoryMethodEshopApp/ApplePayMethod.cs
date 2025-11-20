using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp
{
    // Product
    public class ApplePayMethod : IPaymentMethod
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Successfully paid {amount} to merchant using Apple Pay.");
        }
    }
}
