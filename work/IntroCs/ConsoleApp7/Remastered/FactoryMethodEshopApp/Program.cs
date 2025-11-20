using FactoryMethodEshopApp.SimpleExample;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // A superclass via interface creates object of a subclass (while subclass alter the object creation)
            Checkout ccCheckout = new CreditCardCheckout();
            ccCheckout.Process(129.90);

            Checkout gpCheckout = new GooglePayCheckout();
            gpCheckout.Process(12.50);

        }
    }
}