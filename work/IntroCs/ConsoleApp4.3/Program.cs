namespace ConsoleApp4._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayField game = new PlayField("Quest");
            game.StartGame();

            Player player = game.Player;

            player.PickUpItem();

            game.MovePlayer(1, 0);

            player.PickUpItem();
            foreach (var item in player.Inventory)
            {
                Console.WriteLine(item.Name);
            }

        }
    }
}
