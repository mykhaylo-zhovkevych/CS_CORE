using System;
using ConsoleApp2._3._1;

namespace ConsoleApp2._3._1
{
    internal class TestProgram
    {
        static void Main(string[] args)
        {
            var wagon = new AutomaticWagon(101);

            var food = new Food(101, "Burger", 5);
            var material = new Material(202, "Stone", 5);
            var cloth = new Cloth(303, "Dress", 45);

            var cell1 = new Cell(1, 50);
            var cell2 = new Cell(2, 50);
            var cell3 = new Cell(3, 50);

            cell1.StoreProduct(food);
            cell2.StoreProduct(material);
            cell3.StoreProduct(cloth);

            wagon.Cells.Add(cell1);
            wagon.Cells.Add(cell2);
            wagon.Cells.Add(cell3);

          
            var order1 = new HighPriorityOrder(1001, 2, material);
            var order2 = new LowPriorityOrder(2002, 3, food);
            var order3 = new MiddlePriorityOrder(3003, 5, cloth);

            wagon.PreProcess(order1);
            wagon.PreProcess(order2);
            wagon.PreProcess(order3);

            wagon.ExecuteOrder();

            Console.WriteLine("\n--- Test Ergebnisse ---");
            Console.WriteLine($"Zelle 1 (Food) Restmenge: {food.ProductAmount}");
            Console.WriteLine($"Zelle 2 (Material) Restmenge: {material.ProductAmount}");
            Console.WriteLine($"Zelle 3 (Cloth) Restmenge: {cloth.ProductAmount}");

        }
    }
}
