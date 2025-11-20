using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Factories
{
    public static class IngredientFactory
    {
        public static async Task<Patty> GrillPattyAsync()
        {
            Console.WriteLine("Grilling patty asynchronously...");
            await Task.Delay(20_000);
            return new Patty() { IsGrilled = true };
        }

        public static async Task<Bread> ToastBreadAsync()
        {
            Console.WriteLine("Toasting bread asynchronously...");
            await Task.Delay(5_000);
            return new Bread() { IsToasted = true };
        }

        public static async Task<Cookie> AddACookie()
        {
            Console.WriteLine("Adding a Cookie...");
            await Task.Delay(5_000);
            return new Cookie() { IsServed = true };
        }

        public static async Task<Bacon> GrillBaconAsync()
        {
            Console.WriteLine("Grilling bacon asynchronously...");
            await Task.Delay(10_000);
            return new Bacon() { IsGrilled = true };
        }

        public static async Task<Bread> CoatBreadWithSauceAsync()
        {
            Console.WriteLine("Coating bread with sauce asynchronously...");
            await Task.Delay(2_000);
            return new Bread() { HasSauce = true };
        }

        public static async Task<Sauce> PrepareSauceAsync()
        {
            Console.WriteLine("Preparing sauce asynchronously...");
            await Task.Delay(3_000);
            return new Sauce() { IsCoveredOver = true };
        }
    }
}