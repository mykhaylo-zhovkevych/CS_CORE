using ConsoleApp4._3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    public class KeyboardController : IController
    {
        public PlayerAction GetNextAction()
        {

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.W:
                    return PlayerAction.MoveNorth;

                case ConsoleKey.S:
                    return PlayerAction.MoveSouth;

                case ConsoleKey.A:
                    return PlayerAction.MoveWest;

                case ConsoleKey.D:
                    return PlayerAction.MoveEast;

                case ConsoleKey.E:
                    return PlayerAction.PickUp;

                case ConsoleKey.Q:
                    return PlayerAction.Drop;

                case ConsoleKey.Escape:
                    return PlayerAction.Quit;

                case ConsoleKey.I:
                    return PlayerAction.PrintInventory;

                case ConsoleKey.U:
                    return PlayerAction.Use;

                default:
                    return PlayerAction.None;
            }
        }
    }
}
