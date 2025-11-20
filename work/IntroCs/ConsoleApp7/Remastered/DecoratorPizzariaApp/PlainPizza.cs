namespace DecoratorPizzariaApp
{
    public class PlainPizza : IPizza
    {
        public string PlainPizzaDescription { get; private set; }
        public int PlainPizzaCost { get; private set; }

        public PlainPizza()
        {
            PlainPizzaDescription = "This is Plain Pizza";
            PlainPizzaCost = 5;
        }

        public string CurrentInfoInEng()
        {
            return $"Info in eng: {PlainPizzaDescription}";
        }

        public string CurrentCost()
        {
            return $"Price: {PlainPizzaCost}";
        }
    }
}