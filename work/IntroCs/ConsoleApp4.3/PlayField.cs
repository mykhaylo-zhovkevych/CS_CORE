using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    internal class PlayField
    {

        public Guid Id { get; }
        public string Name { get; }

        private Dictionary<(int x, int y), Field> fields;
        public Player Player { get; set; }

        public PlayField(string name)
        {
            Name = name;
            fields = new Dictionary<(int x, int y), Field>();
        }

        /// <summary>
        /// Starts game with conditions
        /// Player spwons at the random field
        /// </summary>
        public void StartGame()
        {
            

            for (int x = -3; x <= 3; x++)
            {
                for (int y = -3; y <= 3; y++)
                {
                    fields[(x, y)] = new Field($"Field ({x},{y})") { IsWall = false};
                    // Console.WriteLine(fields[(x, y)]);
                }
            }

            fields[(0, 1)].IsWall = true;
            fields[(1, 1)].IsDoor = true;
            fields[(1, 1)].IsLocked = true;
            fields[(1, 1)].DoorTarget = (0, 0);

            fields[(2, 0)].Items.Add(new Key());
            fields[(0, 0)].Items.Add(new Sword());
            fields[(1, 0)].Items.Add(new Box());
            fields[(3, 3)].Items.Add(new Sword());

            Player = new Player("Held", energy: 20, this);
            Player.Position = (0, 0);
            Player.CurrentField = fields[(0, 0)];

        }

        public void MovePlayer(int dx, int dy)
        {

            var newPos = (Player.Position.x + dx, Player.Position.y + dy);
            Field target = fields[newPos];

            if (!fields.ContainsKey(newPos))
            {
                Console.WriteLine("Out of the Map");
                return;
            }

            if (target.IsWall)
            {
                Console.WriteLine("Is a Wall");
                return;
            }

            if (target.IsDoor && target.IsLocked)
            {
                if (Player.Inventory.Any(item => item is Key))
                {
                    target.IsLocked = false;
                    Console.WriteLine($"{Player.Name} has unlocked the Door");

                    var items = Player.Inventory.ToList();
                    var keyToRemove = items.First(item => item is Key);
                    items.Remove(keyToRemove);

                    Player.Inventory.Clear();
                    for (int i = items.Count - 1; i > 0; i--)
                    {
                        Player.Inventory.Push(items[i]);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Door is locked, cannot move.");
                    return;
                }
            }

            if (Player.Energy < 10)
            {
                Console.WriteLine($"{Player.Name} cannot keep moving");
                return;
            }

            Player.Position = newPos;
            Player.CurrentField = target;
            Player.Energy -= 1;

            Console.WriteLine($"Player moved to {Player.Position} ({Player.CurrentField.Name})");

        }

        public void PrintPlayerStack()
        {
               
            
            
        }
    }
}
