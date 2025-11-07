namespace DecoratorPizzariaApp
{
    public class PlainPizza : IPizza
    {

        public string GetDescription()
        {
            return "This is Plain Pizza";
        }
        public double GetCost()
        {
            return 5.00f;
        }
    }
}