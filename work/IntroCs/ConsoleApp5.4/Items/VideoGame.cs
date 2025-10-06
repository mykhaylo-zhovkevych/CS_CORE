using ConsoleApp5._4.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._2.Objects
{
    internal class VideoGame : Item
    {
        public GameType Genre { get; set; }
        public int AgeRating { get; set; }

        public VideoGame(Guid id, string name, GameType genre, int ageRating): base(id, name)
        {
            AgeRating = ageRating;
            Genre = genre;
        }
    }
}
