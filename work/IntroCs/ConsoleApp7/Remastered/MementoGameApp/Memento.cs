namespace MementoGameApp
{
	// This is just Data Object class 
	// It gives a way of retriving the data without allowing direct modification  
	public class Memento
	{
		public int Health { get; private set; }
		public int Mana { get; private set; }
		public int Gold { get; private set; }

        public Memento(int health, int mana, int gold)
		{
            Health = health;
            Mana = mana;
            Gold = gold;
		}
	}
}