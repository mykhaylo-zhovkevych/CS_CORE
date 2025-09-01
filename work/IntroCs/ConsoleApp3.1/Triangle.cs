using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    internal class Triangle : Shape
    {
        public int SideA { get; private set; }
        public int SideB { get; private set; }
        public int SideC { get; private set; }

        public Triangle (int a, int b, int c)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }


        public override string Name { get; set; } = "Triangle";
        public override string Color { get; set; } = "#000000";
        public override Color Background { get; set; } = new Color();
        public override int BorderWidth { get; set; } = 1;

    }
}
