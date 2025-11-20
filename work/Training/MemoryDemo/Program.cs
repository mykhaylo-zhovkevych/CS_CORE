namespace MemoryDemo
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (DemoResource resource = new DemoResource())
            {
                try
                {
                    resource.DoWork();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception caught in Main: {ex.Message}");
                }

            }
        }
    }
}
