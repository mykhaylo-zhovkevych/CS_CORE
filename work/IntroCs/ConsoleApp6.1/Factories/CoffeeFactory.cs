using ConsoleApp6._1.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Factories
{
    public class CoffeeFactory : IFoodFactory
    {
        public async Task<IFoodItem> ProduceAsync(ITaskExecutor worker)
        {
            var cookie = await worker.RunWithCrewRoleAsync(() => IngredientFactory.AddACookie(), Crew.Roles.LineCook);
            Console.WriteLine("Coffee ready!");
            return new Coffee(cookie);
        }
    }
}