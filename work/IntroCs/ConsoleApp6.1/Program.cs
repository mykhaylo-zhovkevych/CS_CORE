namespace ConsoleApp6._1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Crew MyCrew = new Crew();
            Kitchen MyKitchen = new Kitchen(MyCrew);

            Counter MyCounter = new Counter("First Counter");
            Customer MyCustomer = new Customer();


            MyCounter.UserInputTerminal();


            await MyKitchen.PrepareOrderAsync(MyCounter);
                   

            //Console.ReadKey();
        }
    }
}
