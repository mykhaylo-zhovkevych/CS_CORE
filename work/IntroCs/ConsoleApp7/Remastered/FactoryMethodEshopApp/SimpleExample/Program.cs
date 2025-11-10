namespace FactoryMethodEshopApp.SimpleExample
{
    public class Program
    {
        static void Function(string[] args)
        {
            IPayment payment = PaymentFactoryMethod.Create(PaymentMethod.GooglePay);
            payment.Pay(100);

        }
    }
}
