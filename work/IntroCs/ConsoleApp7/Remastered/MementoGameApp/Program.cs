namespace MementoGameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Originator player = new Originator(100,50,0);

            // Helper class 
            CareTaker system = new CareTaker();

      
            system.Save("save01", player.SaveState());

            player.FightMonster();
            player.CastSpell();

     
            system.Save("save02", player.SaveState());

            player.RestoreState(system.Load("save01"));


        }
    }
}
