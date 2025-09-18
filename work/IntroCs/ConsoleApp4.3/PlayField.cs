using ConsoleApp4._3.Fields;
using ConsoleApp4._3.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal class PlayField
    {
        public Guid Id { get; }
        public string Name { get; }

        private Dictionary<(int x, int y), Field> fields;
        // Hard coded Player, secound option with dependency 
        public Player Player { get; set; }

        public PlayField(string name)
        {
            Name = name;
            fields = new Dictionary<(int x, int y), Field>();
            StartGame();
        }

        /// <summary>
        /// Starts game with conditions
        /// Player spwons at the random field
        /// </summary>
        private void StartGame()
        {

            for (int x = -3; x <= 3; x++)
            {
                for (int y = -3; y <= 3; y++)
                {
                    fields[(x, y)] = new Grass($"Field ({x},{y})");
                }
            }

            fields[(0, 1)] = new Wall("Wall");
            fields[(1, 1)] = new Door("Door", (3, 3));
            fields[(2, 2)] = new Enemy("Enemy");

            fields[(2, 0)].Items.Add(new Key());
            fields[(0, 0)].Items.Add(new Food());
            fields[(0, 0)].Items.Add(new Sword());
            fields[(0, 0)].Items.Add(new Bag());
            fields[(0, 0)].Items.Add(new Key());
            fields[(1, 0)].Items.Add(new Food());
            fields[(3, 3)].Items.Add(new Sword());

            Player = new Player("Held", energy: 20);
            Player.Position = (0, 0);

        }

        private void SetPlayerPosition(int dx, int dy)
        {
            try
            {
                var newPos = (Player.Position.x + dx, Player.Position.y + dy);

                if (!fields.TryGetValue(newPos, out Field target))
                {
                    Console.WriteLine("Out of the map.");
                    Player.Position = (0, 0);
                    return;
                }

                if (!target.CanEnter(Player))
                    return;

                if (Player.Energy < 1)
                {
                    Console.WriteLine($"{Player.Name} not enough energy.");
                    return;
                }

                Player.Energy -= 5;
                Player.Position = newPos;

                target.OnEnter(Player);

                Console.WriteLine($"Palyer moved to {Player.Position}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Inventar error: {ex.Message}");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Invalid position - Player is teleported to start");
                Player.Position = (0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }



        public void PickUpItem()
        {
            var field = fields[Player.Position];
            if (!field.Items.Any())
            {
                Console.WriteLine("Nothing to pick up.");
                return;
            }

            var item = field.Items.Last();
            field.Items.Remove(item);
           
            Player.Inventory.Add(item);

            Console.WriteLine($"{Player.Name} picked up {item.Name} at {Player.Position}");
        }



        public void DropItem()
        {
            if (!Player.Inventory.Any())
            {
                Console.WriteLine("Nothing to drop.");
                return;
            }

            var field = fields[Player.Position];

            var item = Player.Inventory.Last();
            field.Items.Add(item);
            Player.Inventory.Remove(item);

            Console.WriteLine($"{Player.Name} dropped {item.Name} at {Player.Position}");

        }

        public void MovePlayer(Direction dir)
        {
            switch (dir)
            {
                case Direction.North: SetPlayerPosition(0, -1); break;
                case Direction.South: SetPlayerPosition(0, 1); break;
                case Direction.East: SetPlayerPosition(1, 0); break;
                case Direction.West: SetPlayerPosition(-1, 0); break;
            }
        }

        public void PrintPlayerInfo()
        {

            var playerName = Player.Name;
            var remainingEnergy = Player.Energy;
            var position = Player.Position;
            var itemCount = Player.Inventory.Count();

            Console.WriteLine($"=== Player Info ===");
            Console.WriteLine($"Name: {playerName}");
            Console.WriteLine($"Energy: {remainingEnergy}");
            Console.WriteLine($"Position: {position}");
            Console.Write($"Items in Inventory: {itemCount} ");

            try
            {
                Console.WriteLine("All Items (top to bottom):");
                foreach (var item in Player.Inventory)
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
