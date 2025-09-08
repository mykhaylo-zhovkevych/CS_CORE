using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    enum Direction { North, South, East, West }

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
                Console.WriteLine("Inventory is empty. Nothing to pickup.");
                return;
            }

            Item item = CurrentField.Items[^1];
            CurrentField.Items.RemoveAt(CurrentField.Items.Count - 1);
            Inventory.Push(item);

            Console.WriteLine($"{Name} has taken {item.Name}.");
        }

        public void DropItem()
        {

            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty. Nothing to drop.");
                return;
            }

            Item item = Inventory.Pop();
            CurrentField.Items.Add(item);

            Console.WriteLine($"{Name} has dropped {item.Name}.");
        }


        public void Move(Direction dir)
        {
            switch(dir)
            {
                case Direction.North: playfield.MovePlayer(0, -1); break;
                case Direction.South: playfield.MovePlayer(0, 1); break;
                case Direction.East: playfield.MovePlayer(1, 0); break;
                case Direction.West: playfield.MovePlayer(-1, 0); break;
            }
        }
    }
}
