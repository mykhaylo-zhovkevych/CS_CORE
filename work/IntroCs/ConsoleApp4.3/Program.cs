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

            player.Move(Direction.East);  
            player.Move(Direction.East);  
            player.PickUpItem();        

            player.Move(Direction.North);
            player.Move(Direction.West);

            player.Move(Direction.South);
            // Door unlocked
            player.Move(Direction.South);


        }
    }
}
