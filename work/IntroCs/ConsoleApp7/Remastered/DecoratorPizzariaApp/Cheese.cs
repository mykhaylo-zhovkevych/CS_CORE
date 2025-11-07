namespace DecoratorPizzariaApp
{
    public class Cheese : ToppingDecorator
    {
        // Passes wrapped object to the base class, like one extra step in the chain, ends in the interface
        public Cheese(IPizza pizza) : base(pizza)
        {
        }

        public override string GetDescription()
        {
            return _pizza.GetDescription() + ", Cheese";
        }

        public override double GetCost()
        {
            return _pizza.GetCost() + 2.00;
        }

    }
}