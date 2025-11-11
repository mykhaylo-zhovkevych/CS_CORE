namespace DecoratorPizzariaApp
{
    // It is like additioal abstraction over interface, so I also have some logic 
    public abstract class ToppingDecorator : IPizza
    {
        protected IPizza _pizza;

        public ToppingDecorator(IPizza pizza)
        {
            _pizza = pizza;
        }
        public abstract string CurrentCost();

        public abstract string CurrentInfoInEng();


    }
}