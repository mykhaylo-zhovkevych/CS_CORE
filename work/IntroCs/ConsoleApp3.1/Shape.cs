using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    internal abstract class Shape : IMovable
    {
        // Abstract properties can not have private setters
        public abstract string Name { get; set; }
        public abstract string Color { get; set; }
        public abstract Color Background { get; set; }
        public abstract int BorderWidth { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
            Console.WriteLine($"{Name} moved to coordinates ({X}, {Y})");
        }

    }
}
