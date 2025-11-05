using FactoryMethodPattern.MVCFramework;

namespace FactoryMethodPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo: Factory Method Pattern\n");

            // 1) Base Controller uses BladeViewEngine by default
            var baseController = new Controller();
            baseController.Render("home.blade.php", new Dictionary<string, object>
            {
                { "User", "Alice" },
                { "Items", 3 }
            });

            // 2) TwigController overrits Factory Method and brings Twing
            Console.WriteLine("\n== TwigController (overrides to Twing) ==");
            var twig = new TwigController();
            twig.Render("home.twig", new Dictionary<string, object>
            {
                { "User", "Bob" },
                { "Items", 5 }
            });

            // 3) OrderController inherits from TwigController, also uses Twing
            Console.WriteLine("\n== OrderController (inherits TwigController) ==");
            var orderController = new OrderController();
            orderController.ListOrders();
            orderController.GetOrder(1);

            Console.WriteLine("\nDemo finished. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
