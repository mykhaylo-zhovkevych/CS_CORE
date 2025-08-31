using System;
using System;

namespace ConsoleApp3._1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ShapeCanvas shapeCanvas = new ShapeCanvas();
            Circle circle = new Circle(5.4)
            {
                Color = "#FF0000",
                BorderWidth = 2,
            };
            Triangle tri = new Triangle(3, 4, 5)
            {
                Color = "#0000FF"
            };
            Rectangle rec = new Rectangle(4, 4)
            {
                Color = "#00FF00",
                BorderWidth = 3
            };

            //shapeCanvas.AddShape(circle);
            //shapeCanvas.AddShape(tri);
            //shapeCanvas.AddShape(rec);

            circle.Move(2, 3);
            tri.Move(-1, 4);
            rec.Move(0, -2);

            Console.WriteLine(rec.IsSquare());

            //var found = shapeCanvas.FindByName("Circle");
            //Console.WriteLine($"Found {found.Name}, {found.Color} ");

            Group group = new Group("MyShapes");
            Group group02 = new Group("MyShapes02");

    
            group.AddShapeToGroup(circle);
            group.AddShapeToGroup(tri);
            group.MoveGroup(1, 1);


            group02.AddShapeToGroup(circle);
            group02.AddShapeToGroup(tri);
            group02.MoveGroup(1, 1);
        }
    }
}