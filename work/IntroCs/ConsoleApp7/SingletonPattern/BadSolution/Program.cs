namespace SingletonPattern.BadSolution
{
    internal class Program
    {
        static void Function(string[] args)
        {

            var settings = new AppSettings();
            settings.Set("app_name", "Design Patterns Mastery");
            settings.Set("app_creator", "Danny");

            Console.WriteLine(settings.Get("app_name"));

            // This will return a null because in the test this value dont exist
            Test.Run();
        }
    }
}