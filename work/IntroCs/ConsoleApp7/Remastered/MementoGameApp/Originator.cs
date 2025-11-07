namespace MementoGameApp
{
    // The feature of restoring and retrieving data happens in here 
    // So like the player needs this features
    // this class dont need to know about the saves
    public class Originator
    {
        private int _health;
        private int _mana;
        private int _gold;


        public Originator(int health, int mana, int gold)
        {
            _health = health;
            _mana = mana;
            _gold = gold;
        }


        public void FightMonster()
        {
            _health -= 10;
            _mana -= 10;
            _gold += 10;
        }

        public void CastSpell()
        {
            _mana -= 15;
            _health += 5;
        }

        // Save and restore state to the memento
        public Memento SaveState()
        {
            Console.WriteLine($"{_health}, {_mana}, {_gold}");
            return new Memento(_health, _mana, _gold);
        }

        public void RestoreState(Memento memento)
        {
            _health = memento.GetHealth();
            _mana = memento.GetMana();
            _gold = memento.GetGold();

            Console.WriteLine($"{_health}, {_mana}, {_gold}");
        }
    }
}