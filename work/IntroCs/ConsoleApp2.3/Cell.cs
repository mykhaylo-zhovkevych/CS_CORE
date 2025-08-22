using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2._3
{
    internal class Cell
    {

        public int CellNumber { get; private set; }
        public int OccupiedSpaces { get; private set; }

        private List<Product> Products = new List<Product>();

        public Cell(int number)
        {
            CellNumber = number;
            OccupiedSpaces = 0;
        }

        public void StoreProduct (Product product)
        {
            Products.Add(product);
            OccupiedSpaces += product.ProductAmount;
        }

       
        public Product RemoveProduct (Product product, int quantity)
        {
            var existing = Products.FirstOrDefault(p => p.ProductNumber == product.ProductNumber);
            
            if (existing != null && existing.ProductAmount >= quantity)
            {
                existing.ProductAmount -= quantity;
                OccupiedSpaces -= quantity;

                return new Product(existing.ProductNumber, existing.Name, quantity);   
            }
            throw new InvalidOperationException("Not enough product in cell to remove.");
        }


    }
}