//using System;
//using System.Collections.Generic;

//namespace ConsoleApp2._1.Example
//{
//    // Prodact
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            var customer = new Customer(1, "Max Mustermann");

//            var order1 = new Order(1001, DateTime.Now);
//            order1.AddPosition(new OrderPosition(5, "Apfel", 0.5f));
//            order1.AddPosition(new OrderPosition(2, "Brot", 2.0f));

//            var order2 = new Order(1002, DateTime.Now);
//            order2.AddPosition(new OrderPosition(3, "Milch", 1.2f));

//            customer.AddOrder(order1);
//            customer.AddOrder(order2);

//            Console.WriteLine(customer);
//            foreach (var order in customer.GetOrders())
//            {
//                Console.WriteLine(order);
//            }
//        }
//    }
//}
