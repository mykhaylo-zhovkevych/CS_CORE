using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static ConsoleApp2._3._1.Warehouse;

namespace ConsoleApp2._3._1
{
    internal class Cell
    {

        public int CellNumber { get; private set; }
        private List<Product> Products = new List<Product>();

        // cunstructor
        public Cell(int number) => CellNumber = number;
  

        public void StoreProduct(Product product)
        {
            Products.Add(product);
        }

        public Product RemoveProduct(Product product, int quantity)
        {
            var existing = Products.FirstOrDefault(p => p.ProductNumber == product.ProductNumber);

            if (existing != null && existing.ProductAmount >= quantity)
            {
                existing.ProductAmount -= quantity;
                return new Product(existing.ProductNumber, existing.Name, quantity);
            }

            throw new InvalidOperationException("Not enough product in cell to remove.");
        }

        public bool HasEnoughProduct(Product product, int quantity)
        {
            var existing = Products.FirstOrDefault(p => p.ProductNumber == product.ProductNumber);
            return existing != null && existing.ProductAmount >= quantity;
        }

        public bool HasSpaceForProduct(int quantity)
        {
            var total = Products.Sum(p => p.ProductAmount);
            return total > quantity;
        }

    }
}
