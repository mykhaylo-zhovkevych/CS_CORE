namespace MementoGameApp
{
    // The feature of restoring and retrieving data happens in here 
    // So like the player needs this features
    // this class dont need to know about the saves
    public class Originator
    {
        public int Health { get; private set; }
        public int Mana { get; private set; }
        public int Gold { get; private set; }


        public Originator(int health, int mana, int gold)
        {
            Health = health;
            Mana = mana;
            Gold = gold;
        }


        public void FightMonster()
        {
            Health -= 10;
            Mana -= 10;
            Gold += 10;
        }

        public void CastSpell()
        {
            Mana -= 15;
            Health += 5;
        }

        // Save and restore state to the memento
        public Memento SaveState()
        {
            Console.WriteLine($"{Health}, {Mana}, {Gold}");
            return new Memento(Health, Mana, Gold);
        }

        public void RestoreState(Memento memento)
        {
            Health = memento.Health;
            Mana = memento.Mana;
            Gold = memento.Gold;

            Console.WriteLine($"{memento.Health.ToString()}, {memento.Mana.ToString()}, {memento.Gold.ToString()}");
        }
    }
}