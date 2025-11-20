using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp
{
    public class CreditCardCheckout : Checkout
    {
        protected override IPaymentMethod CreatePaymentMethod() => new CreditCardMethod();
        
    }
}
