using ConsoleApp5._4.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Objects
{
    public class BoardGame : Item
    {
        public int AgeRating { get; set; }
        public GameType Genre { get; set; }

        public BoardGame(Guid id, string name, int ageRating, GameType genre)
            : base(id, name)
        {
            AgeRating = ageRating;
            Genre = genre;
        }
    }
}
