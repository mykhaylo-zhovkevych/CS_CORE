using ConsoleApp4._3.Interfaces;
using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    public class Player 
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

            if (item is IConsumable consumable)
            {
                // dependent on user 
                consumable.Consume(this);
                _inventoryIndex++;
            }

            else if (item is IUsable usable)
            {
                usable.Use(this);
                _inventoryIndex++;
            }
        }

        public void PrintPlayerInventory()
        {
            try
            {
                Console.WriteLine("All Items (top to bottom):");
                foreach (var item in Inventory)
                {
                    Console.WriteLine($" -  {item.Name}");
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Inventory is empty");
            }
        }
    }
}
