namespace ConsoleApp6
{
    public class Program
    {
        static void Main(string[] args)
        {

            Printer printer = new Printer();

            Client client01 = new Client("Test Worker Laptop", printer);
            Client client02 = new Client("HR Desktop", printer);
            Client client03 = new Client("IT Desktop", printer);



        }
    }
}
