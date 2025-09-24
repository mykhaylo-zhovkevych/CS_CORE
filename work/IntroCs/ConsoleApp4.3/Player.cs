using ConsoleApp4._3.Interfaces;
using ConsoleApp4._3.Items;
using ConsoleApp4._3.OutputServices;
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
        private readonly IOutputService _outputService = new ConsoleOutputService();

        public Player(string name, int energy)
        {
            Name = name;
            Energy = energy;
           
        }

        public void UseTopItem()
        {
            if (!Inventory.Any())
            {
                _outputService.WriteLine("Inventory is empty");
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
                _outputService.WriteLine("All Items (top to bottom):");
                foreach (var item in Inventory)
                {
                    _outputService.WriteLine($" -  {item.Name}");
                }
            }
            catch (InvalidOperationException)
            {
                _outputService.WriteLine("Inventory is empty");
            }
        }
    }
}
