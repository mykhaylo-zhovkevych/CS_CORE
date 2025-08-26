using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    internal class Hall
    {
        public string HallName { get; set; }
        public int Floor { get; set; }
        public List<SeatingPlan> SeatingPlans { get; set; } = new List<SeatingPlan>();

    }
}
