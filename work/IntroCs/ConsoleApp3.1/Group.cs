using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._1
{
    internal class Group : IMovable
    {
        public string GroupName { get; set; }
        public List<Shape> Shapes { get; set; } = new List<Shape>();

        public Group(string groupName)
        {
            GroupName = groupName;
        }

        public Group MoveGroup(int dx, int dy)
        {
            foreach (var shape in Shapes)
            {
                shape.Move(dx, dy);
            }
            return this;
        }

        public void AddShapeToGroup(Shape shape)
        {

            Shapes.Add(shape);
            Console.WriteLine($"Shape {shape.Name} added to group {GroupName}.");
        }

        public void RemoveShapeFromGroup(Shape shape)
        {
            Shapes.Remove(shape);
        
        }


    }
}
