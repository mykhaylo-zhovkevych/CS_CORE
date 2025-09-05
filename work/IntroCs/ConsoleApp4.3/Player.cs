using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal class Player 
    {
        public string Name { get; }
        public int Energy { get; set; }
        public (int x, int y) Position { get; set; }

        public Field CurrentField { get; set; }

        public Stack<Item> Inventory { get; } = new Stack<Item>();

        private PlayField playfield;

        public Player(string name, int energy, PlayField field)
        {
            Name = name;
            Energy = energy;
            playfield = field;
        }

        public void PickUpItem()
        {
            if (CurrentField.Items.Count == 0 )
            {
                return;
            }

            Item item = CurrentField.Items[^1];
            CurrentField.Items.RemoveAt(CurrentField.Items.Count - 1);
            Inventory.Push(item);

            Console.WriteLine($"{Name} has taken {item.Name}.");
        }


    }
}
