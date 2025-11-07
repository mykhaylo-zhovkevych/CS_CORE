namespace MementoGameApp
{
	// This is just Data Object class 
	// It gives a way of retriving the data without allowing direct modification  
	public class Memento
	{
		private int _health;
		private int _mana;
		private int _gold;

		public Memento(int health, int mana, int gold)
		{
			_health = health;
			_mana = mana;
			_gold = gold;
		}

		internal int GetHealth()
		{
			return _health;
		}
		internal int GetMana()
		{
			return _mana;
		}
		internal int GetGold()
		{
			return _gold;
		}
	}
}