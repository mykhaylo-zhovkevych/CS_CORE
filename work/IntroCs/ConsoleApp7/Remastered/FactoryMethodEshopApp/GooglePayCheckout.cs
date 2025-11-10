using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp
{
    public class GooglePayCheckout : Checkout
    {
        protected override IPaymentMethod CreatePaymentMethod() => new GooglePayMethod();
    }
}
