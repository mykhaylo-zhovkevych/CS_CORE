using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp2._3._1
{
    internal class Cell
    {
        private List<Product> Products = new List<Product>();

        public int Id { get; private set; }
        public int MaxCapacity { get; private set; }

        public Cell (int id, int maxCapacity)
        {
            this.Id = id;
            this.MaxCapacity = maxCapacity;

        }

        public void StoreProduct(Product product)
        {
            if (!HasEnoughFreeSpace(product.ProductAmount))
                throw new InvalidOperationException($"Cannot store {product.ProductAmount} of {product.Name} in Cell {Id}: not enough space");

            Products.Add(product);
        }
        
        public Product RemoveProduct(Product product, int quantity)
        {
            var existing = Products.FirstOrDefault(p => p.Id == product.Id);

            if (existing != null && existing.ProductAmount >= quantity)
            {
                existing.ProductAmount -= quantity;

                var type = existing.GetType();
                return (Product)Activator.CreateInstance(type, existing.Id, existing.Name, quantity);
            }

            throw new InvalidOperationException("Not enough product in cell to remove.");
        }


        public bool HasEnoughProduct(Product product, int quantity)
        {
            var existing = Products.FirstOrDefault(p => p.Id == product.Id);
            return existing != null && existing.ProductAmount >= quantity;
        }

        public bool HasEnoughFreeSpace(int quantity)
        {
            var usedSpace = Products.Sum(p => p.ProductAmount);
            return (usedSpace + quantity) <= MaxCapacity;
        }  
    }
}
