namespace AdapterPattern
{ 

    public class BlackAndWhiteColor : IColor
    {
        public void Apply(Video video)
        {
            Console.WriteLine("Applying Black and White Color Filter");
        }
    }
}