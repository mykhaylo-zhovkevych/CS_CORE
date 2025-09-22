using ConsoleApp4._3.Exceptions;
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
using static ConsoleApp4._3.Exceptions.OutOfBoundsException;


namespace ConsoleApp4._3
{
    public class PlayField 
    {
        private readonly IController _controller;
        public Guid Id { get; }
        public string Name { get; }
        // look at the Implantation 
        public Dictionary<(int x, int y), Field> Fields { get; }
        // Hard coded Player, secound option with dependency 
        public Player Player { get; set; }

        public PlayField(string name, IController controller)
        {
            Name = name;
            _controller = controller;
            Fields = new Dictionary<(int x, int y), Field>();
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
                    Fields[(x, y)] = new Grass($"Field ({x},{y})");
                }
            }

            Fields[(0, 1)] = new Wall("Wall");
            Fields[(1, 3)] = new Door("Door", (3, 3));
            Fields[(6, 2)] = new Enemy("Enemy");

            Fields[(2, 0)].Items.Add(new Key());
            Fields[(0, 0)].Items.Add(new Food());
            Fields[(0, 0)].Items.Add(new Sword());
            Fields[(0, 0)].Items.Add(new Bag());
            Fields[(0, 0)].Items.Add(new Key());
            Fields[(1, 0)].Items.Add(new Food());
            Fields[(3, 3)].Items.Add(new Sword());

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
            var allCoords = Fields.Keys;

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
            return Fields.TryGetValue(pos, out field);
        }

        private void SetPlayerPosition(int dx, int dy)
        {
            try
            {
                var newPos = (Player.Position.x + dx, Player.Position.y + dy);

                if (!Fields.TryGetValue(newPos, out Field target))
                {
                    throw new OutOfBoundsException(newPos);
                }

                if (!target.MovePlayerToField(Player))
                {
                    return;
                }

                if (Player.Energy < 1)
                {
                    Console.WriteLine($"{Player.Name} has not enough energy.");
                    return;
                }

                Player.Energy -= 5;
                Player.Position = newPos;


                Console.WriteLine($"Palyer moved to {Player.Position}");
            }
            catch (OutOfBoundsException ex)
            {
                Console.WriteLine($"{ex.Message}");
                //Player.Position = (0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        public void PickUpItem()
        {
            var field = Fields[Player.Position];
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

            var field = Fields[Player.Position];

            var item = Player.Inventory.Last();
            field.Items.Add(item);
            Player.Inventory.Remove(item);

            Console.WriteLine($"{Player.Name} dropped {item.Name} at {Player.Position}");

        }
    }
}
