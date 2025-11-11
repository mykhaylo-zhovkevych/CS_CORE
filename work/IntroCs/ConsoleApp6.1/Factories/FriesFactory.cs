using ConsoleApp6._1.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1.Factories
{
    public class FriesFactory : IFoodFactory
    {
        public async Task<IFoodItem> ProduceAsync(ITaskExecutor worker)
        {
            var sauce = await worker.RunWithCrewRoleAsync(() => IngredientFactory.PrepareSauceAsync(), Crew.Roles.LineCook);
            Console.WriteLine("Fries ready!");
            return new Fries(sauce);
        }
    }
}
