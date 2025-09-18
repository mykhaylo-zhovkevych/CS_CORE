using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal class Player 
    {
        private int _inventoryIndex = 0;
        public string Name { get; }
        public int Energy { get; set; }
        public (int x, int y) Position { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, int energy)
        {
            Name = name;
            Energy = energy;
           
        }

        public void UseTopItem()
        {
            if (!Inventory.Any())
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            if (_inventoryIndex >= Inventory.Count)
            {
                // reset
                _inventoryIndex = 0;
            }

            var item = Inventory[_inventoryIndex];

            item.Use(this);

            if (item is Food)
            {
                Inventory.RemoveAt(_inventoryIndex);
            }
            else
            {
                _inventoryIndex++;
            }
        }
    }
}
