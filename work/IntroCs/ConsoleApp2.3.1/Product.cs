using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._3._1
{
    internal abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductAmount { get; set; }

        protected Product(int id, string name, int amount)
        {
            Id = id;
            Name = name;
            ProductAmount = amount;
        }

        public virtual int SumAmountOfType<T>(IEnumerable<Product> products) where T : Product
        {
            return products.OfType<T>().Sum(p => p.ProductAmount);
        }
    }

    internal class Food : Product
    {
        public Food(int id, string name, int amount) : base(id, name, amount) {


        }
    }

    internal class Material : Product
    {
        public Material(int id, string name, int amount) : base(id, name, amount) { }
    }

    internal class Cloth : Product
    {
        public Cloth(int id, string name, int amount) : base(id, name, amount) { }
    }
}
