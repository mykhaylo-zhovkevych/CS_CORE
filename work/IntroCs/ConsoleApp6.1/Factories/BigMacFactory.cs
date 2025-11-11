using System.Threading.Tasks;
using ConsoleApp6._1.Menu;

namespace ConsoleApp6._1.Factories
{
    // Concrete Creator
    public class BigMacFactory : IFoodFactory
    {
        public async Task<IFoodItem> ProduceAsync(ITaskExecutor worker)
        {
            var pattyTask = worker.RunWithCrewRoleAsync(() => IngredientFactory.GrillPattyAsync(), Crew.Roles.Chef);
            var baconTask = worker.RunWithCrewRoleAsync(() => IngredientFactory.GrillBaconAsync(), Crew.Roles.Chef);
            var topBreadTask = worker.RunWithCrewRoleAsync(() => IngredientFactory.ToastBreadAsync(), Crew.Roles.LineCook);
            var bottomBreadTask = worker.RunWithCrewRoleAsync(() => IngredientFactory.CoatBreadWithSauceAsync(), Crew.Roles.LineCook);

            await Task.WhenAll(pattyTask, baconTask, topBreadTask, bottomBreadTask);

            var burger = new BigMac(topBreadTask.Result, bottomBreadTask.Result, baconTask.Result, pattyTask.Result);
            Console.WriteLine("BigMac ready!");
            return burger;
        }
    }
}
