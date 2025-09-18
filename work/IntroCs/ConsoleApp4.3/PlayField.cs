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
                    fields[(x, y)] = new Field($"Field ({x},{y})") { IsWall = false};
                }
            }

            fields[(0, 1)].IsWall = true;
            fields[(1, 1)].IsDoor = true;
            fields[(1, 1)].IsLocked = true;
            fields[(1, 1)].DoorTarget = (3, 3);

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

        public void MovePlayer(int dx, int dy)
        {
            var newPos = (Player.Position.x + dx, Player.Position.y + dy);
            Field target;

            try 
            {
                // When does a error occurs it will not be assigned
                target = fields[newPos];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Out of the Map");
                Player.Position = (0, 0);
                Console.WriteLine("You was teleported to the start");
                return;
            } 

            if (target.IsWall)
            {
                Console.WriteLine("Is a Wall");
                return;
            }

            if (target.IsDoor )
            {
                if (target.IsLocked)
                {
                    if (Player.Inventory.Any(item => item is Key))
                    {
                        target.IsLocked = false;
                        Console.WriteLine($"{Player.Name} has unlocked the Door");

                        var items = Player.Inventory;
                        var keyToRemove = items.First(item => item is Key);
                        items.Remove(keyToRemove);


                        Player.Position = target.DoorTarget;
                        Console.WriteLine($"{Player.Name} was teleported to {target.DoorTarget}");
                        return;

                    }
                    else
                    {
                        Console.WriteLine("Door is locked.");
                        return;
                    }
                }

                Console.WriteLine("Door is not closed, you can move on");

            }

            if (Player.Energy < 10)
            {
                Console.WriteLine($"{Player.Name} cannot keep moving");
                return;
            }

            Player.Position = newPos;
            Player.Energy -= 1;

            Console.WriteLine($"Player moved to {Player.Position}");

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
            field.Items.RemoveAt(field.Items.Count - 1);
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

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.North: MovePlayer(0, -1); break;
                case Direction.South: MovePlayer(0, 1); break;
                case Direction.East: MovePlayer(1, 0); break;
                case Direction.West: MovePlayer(-1, 0); break;
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
