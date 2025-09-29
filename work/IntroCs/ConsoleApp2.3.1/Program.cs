using System;
using ConsoleApp2._3._1;

namespace ConsoleApp2._3._1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var warehouse = new Warehouse();

            var c1 = new Cell(1, 50);
            var c2 = new Cell(2, 50);
            var c3 = new Cell(3, 50);

            warehouse.AddCell(c1);
            warehouse.AddCell(c2);
            warehouse.AddCell(c3);

            Product food = new Food(101, "Burger", 10);
            Product stone = new Material(202, "Stone", 19);
            Product cloth = new Cloth(303, "Dress", 20);

            c1.StoreProduct(food); 
            c2.StoreProduct(stone);  
            c3.StoreProduct(cloth);  

            
            var wagon = new AutomaticWagon(1001);

            Order o1 = new HighPriorityOrder(1, 5, stone, c2, c1);   
            Order o2 = new LowPriorityOrder(2, 4, food, c1, c3);     
            Order o3 = new MiddlePriorityOrder(3, 5, cloth, c3, c2); 

            wagon.PreProcess(o1);
            wagon.PreProcess(o2);
            wagon.PreProcess(o3);

            wagon.ExecuteOrder();


        }
    }
}
