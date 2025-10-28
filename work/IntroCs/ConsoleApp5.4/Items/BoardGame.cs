using ConsoleApp5._4.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Items
{
    public class BoardGame : Item
    {
        public int AgeRating { get; set; }
        public GameType Genre { get; set; }

        public BoardGame(string name, int ageRating, GameType genre): base(name)
        {
            AgeRating = ageRating;
            Genre = genre;
        }
    }
}
