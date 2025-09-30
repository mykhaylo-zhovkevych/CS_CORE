using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Objects
{
    internal class BoardGame : Object
    {
        public int AgeRating { get; set; }
        public GameType Genre { get; set; }

        public BoardGame(Guid id, string name, double basicPrice, double surcharge, int ageRating, GameType genre)
            : base(id, name, basicPrice, surcharge)
        {
            AgeRating = ageRating;
            Genre = genre;
        }

    }
}
