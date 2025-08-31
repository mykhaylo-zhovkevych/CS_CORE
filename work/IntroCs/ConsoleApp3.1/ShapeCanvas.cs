using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    internal class ShapeCanvas
    {

        public List<Shape> Shapes { get; set; } = new List<Shape>();

        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            Shapes.Remove(shape);
        }

        public Shape FindByName(string name)
        {
            return Shapes.FirstOrDefault(s => s.Name.Equals(name, StringComparison.Ordinal));
        }
    }
}
