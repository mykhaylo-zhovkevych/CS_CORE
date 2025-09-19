using ConsoleApp4._3.Fields;
using ConsoleApp4._3.Interfaces;
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

        private readonly IController _controller;
        public Guid Id { get; }
        public string Name { get; }
        // look at the Implantation 
        private Dictionary<(int x, int y), Field> _fields;
        // Hard coded Player, secound option with dependency 
        public Player Player { get; set; }

        public PlayField(string name, IController controller)
        {
            Name = name;
            _controller = controller;
            _fields = new Dictionary<(int x, int y), Field>();
            InitGame();
        }

        /// <summary>
        /// Starts game with conditions
        /// Player spwons at the random field
        /// </summary>
        private void InitGame()
        {

            for (int x = -6; x <= 6; x++)
            {
                for (int y = -3; y <= 3; y++)
                {
                    _fields[(x, y)] = new Grass($"Field ({x},{y})");
                }
            }

            _fields[(0, 1)] = new Wall("Wall");
            _fields[(1, 3)] = new Door("Door", (3, 3));
            _fields[(6, 2)] = new Enemy("Enemy");

            _fields[(2, 0)].Items.Add(new Key());
            _fields[(0, 0)].Items.Add(new Food());
            _fields[(0, 0)].Items.Add(new Sword());
            _fields[(0, 0)].Items.Add(new Bag());
            _fields[(0, 0)].Items.Add(new Key());
            _fields[(1, 0)].Items.Add(new Food());
            _fields[(3, 3)].Items.Add(new Sword());

            Player = new Player("Held", energy: 200);
            Player.Position = (0, 0);

        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                DrawFields();

                var action = _controller.GetNextAction();

                switch (action) 
                {
                    case PlayerAction.PrintInventory: Player.PrintPlayerInventory(); break;
                    case PlayerAction.MoveNorth: MovePlayer(Direction.North); break;
                    case PlayerAction.MoveSouth: MovePlayer(Direction.South); break;
                    case PlayerAction.MoveWest: MovePlayer(Direction.West); break;
                    case PlayerAction.MoveEast: MovePlayer(Direction.East); break;
                    case PlayerAction.Use: Player.UseTopItem(); break;
                    case PlayerAction.PickUp: PickUpItem(); break;
                    case PlayerAction.Drop: DropItem(); break;
                    case PlayerAction.Quit: running = false; break;
                }
            }
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

        private void DrawFields()
        {
            
            var playerPos = Player.Position;
            var allCoords = _fields.Keys;

            int minX = allCoords.Min(c => c.x);
            int maxX = allCoords.Max(c => c.x);
            int minY = allCoords.Min(c => c.y);
            int maxY = allCoords.Max(c => c.y);

            for (int y = minY; y <= maxY; y++)
            {

                for (int x = minX; x <= maxX; x++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");

                for (int x = minX; x <= maxX; x++)
                {
                    char c = ' ';
                    if ((x, y) == playerPos)
                    {
                        c = 'P';
                    }
                       
                    else if (TryGetField((x, y), out var field))
                    {
                        c = field.Symbol;
                    }

                    Console.Write($"| {c} ");
                }
                Console.WriteLine("|");
            }

            for (int x = minX; x <= maxX; x++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
        }

        private bool TryGetField((int x, int y) pos, out Field field)
        {
            return _fields.TryGetValue(pos, out field);
        }

        private void SetPlayerPosition(int dx, int dy)
        {
            try
            {
                var newPos = (Player.Position.x + dx, Player.Position.y + dy);

                if (!_fields.TryGetValue(newPos, out Field target))
                {
                    Console.WriteLine("Out of the map.");
                    Player.Position = (0, 0);
                    return;
                }

                if (!target.CanEnter)
                    return;

                if (Player.Energy < 1)
                {
                    Console.WriteLine($"{Player.Name} has not enough energy.");
                    return;
                }

                Player.Energy -= 5;
                Player.Position = newPos;

                target.OnEnter(Player);

                Console.WriteLine($"Palyer moved to {Player.Position}");
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
            var field = _fields[Player.Position];
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

            var field = _fields[Player.Position];

            var item = Player.Inventory.Last();
            field.Items.Add(item);
            Player.Inventory.Remove(item);

            Console.WriteLine($"{Player.Name} dropped {item.Name} at {Player.Position}");

        }

    }
}
