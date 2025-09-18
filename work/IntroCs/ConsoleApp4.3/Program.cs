using ConsoleApp4._3.Items;

namespace ConsoleApp4._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayField game = new PlayField("Quest");

            Player player = game.Player;

            game.PickUpItem(); 
            game.PickUpItem();
            game.PickUpItem();
            game.PickUpItem();
            player.UseTopItem();
            player.UseTopItem();
            player.UseTopItem();

            player.UseTopItem();

            //Food food = new Food();
            //food.Use(player);

            game.MovePlayer(Direction.East);
            game.MovePlayer(Direction.East);
            game.PickUpItem();

            game.MovePlayer(Direction.North);
            game.MovePlayer(Direction.West);

            game.MovePlayer(Direction.South);
     
            game.MovePlayer(Direction.South);
            game.DropItem();
            game.DropItem();

            game.PrintPlayerInfo();
        }
    }
}
