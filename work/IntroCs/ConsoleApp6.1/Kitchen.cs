using ConsoleApp6._1.Menu;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace ConsoleApp6._1
{
    // Interface is used for better extensibility like later Italian Kitchen etc
    public class Kitchen : ITaskExecutor
    {
        private readonly SemaphoreSlim _crewSemaphore;

        public string KitchenName { get; private set; }
        public Crew CurrentCrew { get; private set; }

        public Kitchen(Crew currentCrew)
        {
            KitchenName = "Main Kitchen";
            CurrentCrew = currentCrew;
            _crewSemaphore = new SemaphoreSlim(currentCrew.Members.Count);

        }

        public async Task PrepareOrderAsync(Counter counter)
        {
            while (counter.PendingOrders.TryDequeue(out var order))
            {
                Console.WriteLine($"Order from: {counter.CounterName}");
                await ProcessAsync(order);
            }
        }

        private async Task ProcessAsync(Order order)
        {
            Console.WriteLine($"Process started your, ID: {order.OrderId}");

            var tasks = order.OrderAmount.Select(item =>
            item.Factory.ProduceAsync(this)).ToList();

            await Task.WhenAll(tasks);
            Console.WriteLine($"{KitchenName} has finished the process for this {order.OrderId}");
        }

        public async Task<T> RunWithCrewRoleAsync<T>(Func<Task<T>> func, Crew.Roles requiredRole)
        {
            await _crewSemaphore.WaitAsync();
            var availableMember = CurrentCrew.Members.First(n => Equals(n.Role, requiredRole));
            if (availableMember is null)
            {
                _crewSemaphore.Release();
                await Task.Delay(100);
                throw new Exception($"No crew member available with role {requiredRole}");
            }

            Console.WriteLine($"{availableMember.Name} {requiredRole} starts task");
            var result = await func();
            Console.WriteLine($"{availableMember.Name} {requiredRole} finished task");
            _crewSemaphore.Release();
            return result;

        }
    }
}