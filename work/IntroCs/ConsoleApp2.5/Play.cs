using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    public class Play
    {

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int Duration { get; private set; }
        //public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Play(int id, string title, string author, int duration)
        {
            Id = id;
            Title = title;
            Author = author;
            Duration = duration;
        }

    }
}
