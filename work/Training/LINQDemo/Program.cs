namespace LINQDemo
{


    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var individuals = new List<Person>()
            {
                new Person { Name = "Anna", Age = 34},
                new Person { Name = "Bernd", Age = 40 },
                new Person { Name = "Clara", Age = 30 }

            };   
            
            var ab30 = from i in individuals
                       where i.Age >= 30
                       orderby i.Age
                       select i;

            var ab30Method = individuals
                            .Where(i => i.Age >= 30)
                            .OrderBy(i => i.Age);

            // Or ab30Method
            foreach (var i in ab30)
            {
                Console.WriteLine($"{i.Name} ({i.Age})");
            }

        }
    }
}
