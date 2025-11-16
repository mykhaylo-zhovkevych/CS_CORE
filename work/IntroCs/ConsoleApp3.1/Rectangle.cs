using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    public class Rectangle : Shape
    {

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string Name { get; set; } = "Rectangle";
        public override string Color { get; set; } = "#000000";
        public override Color Background { get; set; } = new Color();
        public override int BorderWidth { get; set; } = 1;

        public bool IsSquare()
        {
            return Width == Height;
        }
    }
}
