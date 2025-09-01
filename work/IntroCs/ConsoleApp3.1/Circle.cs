using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    internal class Circle : Shape
    {

        public double Radius { get; private set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override string Name { get; set; } = "Circle";
        public override string Color { get; set; } = "0000000";
        public override Color Background { get; set; } = new Color();
        public override int BorderWidth { get; set; } = 1;

    }
}
