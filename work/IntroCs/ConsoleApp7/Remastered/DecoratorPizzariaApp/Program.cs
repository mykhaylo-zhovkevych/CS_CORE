namespace DecoratorPizzariaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPizza plainPizza = new PlainPizza();

            // Console.WriteLine("Plain Pizza:");

            IPizza cheesePizza = new Cheese(plainPizza);

            IPizza pepperoniPizza = new Papperoni(cheesePizza);

            Console.WriteLine($"{plainPizza.GetCost()} {plainPizza.GetDescription()}");
            
            Console.WriteLine($"{cheesePizza.GetCost()} {cheesePizza.GetDescription()}");

            Console.WriteLine($"{pepperoniPizza.GetCost()} {pepperoniPizza.GetDescription()}");
           
        }
    }
}
