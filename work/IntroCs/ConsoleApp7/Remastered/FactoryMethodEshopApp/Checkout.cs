using FactoryMethodEshopApp.SimpleExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp
{
    public abstract class Checkout
    {
        // Factory Method
        protected abstract IPaymentMethod CreatePaymentMethod();

        public void Process(double amount)
        {
            // It returns a concrete payment method because of the Factory Method
            var paymentMethod = CreatePaymentMethod();
            paymentMethod.Pay(amount);

        }
    }
}
