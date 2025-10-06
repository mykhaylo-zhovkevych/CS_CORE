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
        public IReadOnlyList<Product> Products => _products;
        public int Id { get; }
        public int MaxCapacity { get; }

        public Cell(int id, int maxCapacity)
        {
            Id = id;
            MaxCapacity = maxCapacity;
        }


        public void StoreProduct(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id );
            HasEnoughFreeSpace(product.ProductAmount);

            if (existing != null)
            {
                existing.ProductAmount += product.ProductAmount;
            }
            else
            {
                _products.Add(product);
            }
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
            product.ProductAmount = quantity;
            return product;
        }

        // rethink
        private void HasEnoughProduct(Product? existingProduct, int quantity)
        {
            if (existingProduct == null || existingProduct.ProductAmount < quantity)
                throw new InvalidOperationException($"Cell {Id}: not enough product available");
        }

        private void HasEnoughFreeSpace(int quantity)
        {
            if (_products.Sum(p => p.ProductAmount) + quantity > MaxCapacity)
                throw new InvalidOperationException($"Cell {Id}: Not enough free space to store {quantity} items");
        }

        public override string ToString()
        {
            if (_products.Count == 0)
            {
                return $"Cell {Id} (Capacity {MaxCapacity}): empty";
            }
            var productList = string.Join(", ", _products.Select(p => $"{p.ProductAmount}x {p.Name}"));
            var used = _products.Sum(p => p.ProductAmount);
            return $"Cell {Id} (Used {used}/{MaxCapacity}): {productList}";
        }
    }
}
