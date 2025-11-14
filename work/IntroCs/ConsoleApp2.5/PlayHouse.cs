using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    public class PlayHouse
    {
        public string PlayHouseName { get; set; }
        public string Loation { get; set; }

        // Association, weil kennt Plays und Halls
        public List<Hall> Halls { get; set; } = new List<Hall>();
        public List<Play> Plays { get; set; } = new List<Play>();
    }
}