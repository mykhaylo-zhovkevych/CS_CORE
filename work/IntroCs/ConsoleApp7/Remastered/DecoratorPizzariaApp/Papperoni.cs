namespace DecoratorPizzariaApp
{
    public class Papperoni : ToppingDecorator
    {
        public Papperoni(IPizza pizza) : base(pizza)
        {
        }

        public override double GetCost()
        {
            return _pizza.GetCost() + 2.00;
        }

        public override string GetDescription()
        {
            return _pizza.GetDescription() + ", Papperoni";
        }
    }
}