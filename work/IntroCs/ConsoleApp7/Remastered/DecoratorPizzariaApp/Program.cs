namespace DecoratorPizzariaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPizza plainPizza = new PlainPizza();


            PlainPizza pp = new PlainPizza();

            // Console.WriteLine("Plain Pizza:");

            IPizza cheesePizza = new Cheese(plainPizza);

            IPizza pepperoniPizza = new Papperoni(cheesePizza);


            Console.WriteLine($"{plainPizza.CurrentCost()} {plainPizza.CurrentInfoInEng()}");
            
            Console.WriteLine($"{cheesePizza.CurrentCost()} {cheesePizza.CurrentInfoInEng()}");

            Console.WriteLine($"{pepperoniPizza.CurrentCost()} {pepperoniPizza.CurrentInfoInEng()}");
           
        }
    }
}
