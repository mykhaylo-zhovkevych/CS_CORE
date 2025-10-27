namespace ConsoleApp6._1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Crew MyCrew = new Crew();
            Kitchen MyKitchen = new Kitchen(MyCrew);

            Counter MyCounter = new Counter("First Counter");

            // Simulation of multiple user inputs
            MyCounter.UserInputTerminal();
            MyCounter.UserInputTerminal();

            await MyKitchen.PrepareOrderAsync(MyCounter);
                   

            // Secound resta

            Console.ReadKey();
        }
    }
}
