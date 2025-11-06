using System.Numerics;

namespace CompositionPattern
{
    // Composite Class
    public class Box : IItems
    {

        private List<IItems> _items = new List<IItems>();

        public void Add(IItems item)
        {
            _items.Add(item);
        }

        public float GetPrice()
        {
            // No need to check the type of the item all can be treated uniformly
            float total = 0.0f;
            foreach (IItems item in _items)
            {
                total += item.GetPrice();
            }
            return total;
        }
    }
}