using System;
using System.Threading.Tasks;
using ConsoleApp6._1;

public interface ITaskExecutor
{
    Task<T> RunWithCrewRoleAsync<T>(Func<Task<T>> func, Crew.Roles requiredRole);
}