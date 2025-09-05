using System.Security.Cryptography.X509Certificates;

namespace GenericsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // What are the Generics?
            // Generics allow you to write type-safe, reusable code that works with any data type.
            // They enable you to define classes, interfaces, methods, delegates, etc. with a placeholder
            // for the data type, which gets specified when the code is used.
            // Benefits: Type safety at compile-time, better performance (no boxing/unboxing), code reusability.

            // Generic Types and Generic Methods
            // 1. Generic Types: Classes, interfaces, structs, delegates that use type parameters
            // 2. Generic Methods: Methods that define their own type parameters independent of the containing class

            // Code Example for Generic Types
            // Example 1: Generic Class
            Box<int> intBox = new Box<int>(42);
            Box<string> stringBox = new Box<string>("Hello Generics");
            Console.WriteLine($"Integer box contains: {intBox.Value}");
            Console.WriteLine($"String box contains: {stringBox.Value}");

            // It means that both variables has it is own static field
            //public class Box<T> 
            //{

            //public static int Instances;
            //public T Value { get; }
            //public Box(T value) { Value = value; Instances++; }

            //}

            //Console.WriteLine(Box<int>.Instances);    // 1
            //Console.WriteLine(Box<string>.Instances);    // 1


            // Example 2: Built-in Generic Collections
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            Dictionary<string, int> ages = new Dictionary<string, int>
            {
                { "Alice", 30 },
                { "Bob", 25 }
            };

            // Code Example for Generic Methods
            // Example 1: Simple generic method
            int[] intArray = { 1, 2, 3, 4, 5 };
            string[] stringArray = { "apple", "banana", "cherry" };

            DisplayArray<int>(intArray);
            DisplayArray<string>(stringArray);
            // Type inference - compiler can figure out the type
            DisplayArray(intArray); // No need to specify <int>

            // Example 2: Generic method with constraints
            Console.WriteLine($"Max of 10 and 20: {GetMax(10, 20)}");
            Console.WriteLine($"Max of 'A' and 'Z': {GetMax('A', 'Z')}");

            // Example 3: Multiple type parameters
            var result = CreatePair("Hello", 42);
            Console.WriteLine($"Pair: {result.Item1}, {result.Item2}");

            // What are some advanced topics that I can take a look at?
            // 1. Generic Constraints (where T : class, new(), IComparable<T>, etc.)
            //    - Restrict type parameters to certain types or capabilities
            //    - Examples: where T : class (reference types only), where T : struct (value types only)
            //    - where T : new() (must have parameterless constructor)
            //    - where T : IComparable<T> (must implement specific interface)

            // 2. Covariance and Contravariance (out and in keywords)
            //    - Covariance (out): Allows using a more derived type (IEnumerable<out T>)
            //    - Contravariance (in): Allows using a less derived type (Action<in T>)

            // 3. Generic Delegates (Func<T>, Action<T>, Predicate<T>)
            //    - Func<T, TResult>: Takes T, returns TResult
            //    - Action<T>: Takes T, returns void
            //    - Predicate<T>: Takes T, returns bool

            // 4. Generic Interfaces (IComparable<T>, IEnumerable<T>, IList<T>)

            string description = "A file with example created";

            // 5. Default Values in Generics (default(T) or default keyword)

            // 6. Generic Type Inference and Method Overloading

            // 7. Reflection with Generics (typeof(T), GetGenericTypeDefinition())

            // 8. Generic Collections and LINQ
            //    - Deep dive into List<T>, Dictionary<TKey, TValue>, Queue<T>, Stack<T>
            //    - LINQ methods are heavily generic-based

            // 9. Custom Generic Data Structures (Trees, Graphs, etc.)

            // 10. Performance Considerations
            //     - Value types vs Reference types in generics
            //     - Boxing/Unboxing avoidance
            //     - JIT compilation for each type parameter combination
        }

        // Generic Class Example
        public class Box<T>
        {
            public T Value { get; set; }

            public Box(T value)
            {
                Value = value;
            }
        }

        // Generic Method Examples
        static void DisplayArray<T>(T[] array)
        {
            Console.WriteLine($"Array of {typeof(T).Name}: {string.Join(", ", array)}");
        }

        // Generic method with constraint
        static T GetMax<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) > 0 ? first : second;
        }

        // Multiple type parameters
        static Tuple<T1, T2> CreatePair<T1, T2>(T1 first, T2 second)
        {
            return new Tuple<T1, T2>(first, second);
        }
    }
}