using ConsoleApp4._3.Items;
using ConsoleApp4._3.OutputServices;

namespace ConsoleApp4._3
{
    public class Program
    {
        static void Main(string[] args)
        {
            KeyboardController controller = new KeyboardController();
            ConsoleOutputService output = new ConsoleOutputService();
            StringBuilderOutputService stringOutput = new StringBuilderOutputService();
            Player player = new Player("Hero", 100);

            PlayField game = new PlayField(player: player, name: "Quest", controller: controller, output: stringOutput);
            game.Run();

        }
    }
}
