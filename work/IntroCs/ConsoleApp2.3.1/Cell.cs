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
            var existing = _products.FirstOrDefault(p => p.Id == product.Id && p.GetType() == product.GetType());
            HasEnoughFreeSpace(product.ProductAmount);

            if (existing != null)
                existing.ProductAmount += product.ProductAmount;
            else
                _products.Add(product);
        }

        public Product RemoveProduct(Product product)
        {
            var quantity = product.ProductAmount;

            var existing = _products.FirstOrDefault(p => p.Id == product.Id );
            HasEnoughProduct(existing, quantity);

            if (existing.ProductAmount == quantity)
            {
                _products.Remove(existing);
                return existing; 
            }

            existing.ProductAmount -= quantity;
            return existing.WithAmount(quantity);
        }

        // rethink
        private void HasEnoughProduct(Product product, int quantity)
        {
            if (product == null || product.ProductAmount < quantity)
                throw new InvalidOperationException($"Cell {Id}: not enough available");
        }

        private void HasEnoughFreeSpace(int quantity)
        {
            if (_products.Sum(p => p.ProductAmount) + quantity > MaxCapacity)
                throw new InvalidOperationException($"Cell {Id}: Not enough free space to store {quantity} items");
        }
    }
}
