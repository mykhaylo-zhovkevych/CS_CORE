using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    public abstract class Product
    {
        public int Id { get; }
        public string Name { get; }
        public int ProductAmount { get; set; }

        protected Product(int id, string name, int amount)
        {
            Id = id;
            Name = name;
            ProductAmount = amount;
        }

    }

    public class Food : Product 
    { 
        public Food(int id, string name, int amount) : base(id, name, amount) { }
   
    }
    public class Material : Product 
    { 
        public Material(int id, string name, int amount) : base(id, name, amount) { }
    
    }
    public class Cloth : Product 
    { 
        public Cloth(int id, string name, int amount) : base(id, name, amount) { }
      
    }
}
