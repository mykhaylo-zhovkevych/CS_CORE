namespace AdapterPattern.Package
{ 

    public class Rainbow ()
    {

        public void Setup()
        {
            Console.WriteLine("Setting up Rainbow filter");
        }

        public void Update(Video video)
        {
            Console.WriteLine("Applying rainbow filter to video");
        }
    }
}