using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Objects
{
    internal class VideoGame : Object
    {
        public GameType Genre { get; set; }
        public int AgeRating { get; set; }

        public VideoGame(Guid id, string name, double basicPrice, double surcharge, int ageRating, GameType genre)
            : base(id, name, basicPrice, surcharge)
        {
            AgeRating = ageRating;
            Genre = genre;
        }
    }
}
