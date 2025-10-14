using ConsoleApp5._4.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Items
{
    public class VideoGame : Item
    {
        public GameType Genre { get; set; }
        public int AgeRating { get; set; }

        public VideoGame(string name, GameType genre, int ageRating): base(name)
        {
            AgeRating = ageRating;
            Genre = genre;
        }
    }
}
