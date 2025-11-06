namespace CompositionPattern
{
    public class Program
    {
        static void Main(string[] args)
        {

            var package = new Box();

            var box1 = new Box();
            box1.Add(new Microphone());

            var box2 = new Box();

            var box3 = new Box();
            box3.Add(new Mouse());

            var box4 = new Box();
            box4.Add(new KeyBoard());
            

            box2.Add(box3);
            box2.Add(box4);

            package.Add(box1);
            package.Add(box2);

            Console.WriteLine($"Total Price : {package.GetPrice()}");

        }
    }
}
