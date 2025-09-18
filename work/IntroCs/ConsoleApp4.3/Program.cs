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


            game.Move(Direction.East);
            game.Move(Direction.East);
            game.PickUpItem();

            game.Move(Direction.North);
            game.Move(Direction.West);

            game.Move(Direction.South);
     
            game.Move(Direction.South);
            game.DropItem();
            game.DropItem();

            game.PrintPlayerInfo();
        }
    }
}
