using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp2._3._1
{
    public class Cell
    {
        private readonly List<Product> _products = new();

        public int Id { get; }
        public int MaxCapacity { get; }

        public Cell(int id, int maxCapacity)
        {
      
            Id = id;
            MaxCapacity = maxCapacity;
        }

        public void StoreProduct(Product product)
        {
            if (!HasEnoughFreeSpace(product.ProductAmount))
                throw new InvalidOperationException($"Cell {Id}: Not enough free space to store {product.ProductAmount} {product.Name}");

            var existing = _products.FirstOrDefault(p => p.Id == product.Id && p.GetType() == product.GetType());
            if (existing != null)
                existing.ProductAmount += product.ProductAmount;
            else
                _products.Add(product);
        }

        public Product RemoveProduct(Product product)
        {
            int quantity = product.ProductAmount;

            var existing = _products.FirstOrDefault(p => p.Id == product.Id && p.GetType() == product.GetType());
            if (existing != null && existing.ProductAmount > quantity)
                throw new InvalidOperationException($"Cell {Id}: dont have enough {product.Name} available");

            if (existing.ProductAmount == quantity)
            {
                _products.Remove(existing);
                return existing; 
            }

            existing.ProductAmount -= quantity;
            return existing.WithAmount(quantity); 
        }

        public bool HasEnoughProduct(Product product, int quantity)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id && p.GetType() == product.GetType());
            return existing != null && existing.ProductAmount >= quantity;
        }

        public bool HasEnoughFreeSpace(int quantity)
        {
            var usedSpace = _products.Sum(p => p.ProductAmount);
            return (usedSpace + quantity) <= MaxCapacity;
        }
    }
}
