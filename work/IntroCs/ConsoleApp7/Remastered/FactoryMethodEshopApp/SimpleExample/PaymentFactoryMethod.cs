using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodEshopApp.SimpleExample
{
    public class PaymentFactoryMethod
    {

        public static IPayment Create(PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.CreditCard:
                    return new CreditCardPayment();
                case PaymentMethod.GooglePay:
                    return new GooglePlayPayment();
                case PaymentMethod.ApplePay:
                    return new ApplePayPayment();

                default:
                    throw new NotSupportedException($"{paymentMethod} is currently not supported as a payment method.");
            }
        }

    }
}
