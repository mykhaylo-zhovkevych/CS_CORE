namespace DecoratorPizzariaApp
{
    public class Cheese : ToppingDecorator
    {
        private int _extraCost = 3;

        // Passes wrapped object to the base class, like one extra step in the chain, ends in the interface
        public Cheese(IPizza pizza) : base(pizza)
        {
        }
        public override string CurrentCost()
        {
            return $"{_pizza.CurrentCost()} + {_extraCost}";
        }

        public override string CurrentInfoInEng()
        {
            return _pizza.CurrentInfoInEng() + ", Cheese";
        }


    }
}