using ConsoleApp4._3.Items;

namespace ConsoleApp4._3
{
    public class Program
    {
        static void Main(string[] args)
        {
            KeyboardController controller = new KeyboardController();

            PlayField game = new PlayField("Quest", controller);
            game.Run();

        }
    }
}
