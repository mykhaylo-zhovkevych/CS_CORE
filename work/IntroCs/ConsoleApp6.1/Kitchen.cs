using ConsoleApp6._1.Menu;
using ConsoleApp6._1.Menu.Ingredients;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace ConsoleApp6._1
{
    // Interface is used for better extensibility like later Italian Kitchen etc
    public class Kitchen : ITaskExecutor
    {
        private readonly SemaphoreSlim _crewSemaphore;
        private readonly ConcurrentDictionary<Crew.CrewMember, bool> _reserved = new();

        public string KitchenName { get; private set; }
        public Crew CurrentCrew { get; private set; }

        public Kitchen(Crew currentCrew)
        {
            KitchenName = "Main Kitchen";
            CurrentCrew = currentCrew;
            _crewSemaphore = new SemaphoreSlim(currentCrew.Members.Count);

            foreach (var m in CurrentCrew.Members)
                _reserved[m] = false;

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

        // Possible solution 
        // Problem: If 3 memebers and 2 of them chefs than first one will be selected
        // Create a pool like _reserved where the members will be temp stored/reserved

        public async Task<T> RunWithCrewRoleAsync<T>(Func<Task<T>> func, Crew.Roles requiredRole)
        {
            await _crewSemaphore.WaitAsync();

            // Current Member
            Crew.CrewMember member = null;

            // Select all members with required role
            var membersWithRole = CurrentCrew.Members.Where(m => m.Role == requiredRole).ToList();

            // If not found
            if (membersWithRole.Count == 0)
            {
                _crewSemaphore.Release();
                throw new InvalidOperationException($"No crew member with role {requiredRole} is available.");
            }

            while (member == null)
            {
                // If not reserved and if not reserved, reserve one
                foreach (var m in membersWithRole)
                {
                    if (!_reserved[m] && _reserved.TryUpdate(m,true,false))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    await Task.Delay(1000); 
                 
                }

            }

            Console.WriteLine($"{member.Name} {requiredRole} starts task");
            var result = await func();
            Console.WriteLine($"{member.Name} {requiredRole} finished task");

            _reserved[member] = false;
            _crewSemaphore.Release();

            return result;
        }
    }
}