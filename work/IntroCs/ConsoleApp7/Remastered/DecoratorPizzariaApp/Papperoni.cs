namespace DecoratorPizzariaApp
{
    public class Papperoni : ToppingDecorator
    {
        private int _extraCost = 2;

        public Papperoni(IPizza pizza) : base(pizza)
        {
        }

        // Function overriding
        public override string CurrentCost()
        {
            return $"{_pizza.CurrentCost()} + {_extraCost}";
        }

        public override string CurrentInfoInEng()
        {
            return _pizza.CurrentInfoInEng() + ", Papperoni";
        }
    }
}