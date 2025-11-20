namespace AdapterPattern
{

    public class MidnightColor : IColor
    {
        public void Apply(Video video)
        {
            Console.WriteLine("Applying Mid Night Color Filter");
        }
    }
}