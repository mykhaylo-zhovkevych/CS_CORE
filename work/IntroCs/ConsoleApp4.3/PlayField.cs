using System;
using System.Collections.Generic;
using System.Linq;
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
            fields[(0,0)] = new Field("TestField") { IsWall = false};
            fields[(0, 1)] = new Field("Wall") { IsWall = true };
            fields[(1, 0)] = new Field("Room") { IsWall = false };
            fields[(1, 1)] = new Field("Locked Door") { IsDoor = true, IsLocked = true, DoorTarget = (0, 0) };

            foreach (var ((x,y), field) in fields)
            {
                fields.TryGetValue((x, y-1), out field.North);
                fields.TryGetValue((x+1, y), out field.East);
                fields.TryGetValue((x, y+1), out field.South);
                fields.TryGetValue((x - 1, y), out field.West);
            }

            fields[(0, 0)].Items.Add(new Key());
            fields[(0, 0)].Items.Add(new Sword());
            fields[(1, 0)].Items.Add(new Box());

            Player = new Player("Held", energy: 20, this);
            Player.Position = (0,0);
            Player.CurrentField = fields[(0, 0)];
        }

        public void MovePlayer(int dx, int dy)
        {
            var newPos = (Player.Position.x + dx, Player.Position.y + dy);
            if (!fields.ContainsKey(newPos))
                return;
            Field target = fields[newPos];
            if (target.IsWall)
                return;

            if (target.IsDoor && target.IsLocked)
            {
                if (Player.Inventory.Any(item => item is Key))
                {
                    target.IsLocked = false;
                    //
                }
                else
                {
                    // doors are closed
                    return;
                }
            }
            Player.Energy -= 1;

            if (target.IsDoor)
            {
                Player.Position = target.DoorTarget;
                Player.CurrentField = fields[target.DoorTarget]; 
            }
            else
            {
                Player.Position = newPos;
                Player.CurrentField = target; 
            }
        }
    }
}
