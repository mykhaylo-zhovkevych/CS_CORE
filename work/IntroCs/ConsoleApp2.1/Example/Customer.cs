using System.Collections.Generic;

namespace ConsoleApp2._1.Example
{
    class Customer
    {
        private int _id;
        private string _name;
        private List<Order> _orders = new List<Order>();

        public Customer(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> GetOrders()
        {
            return _orders;
        }

        public override string ToString()
        {
            return $"Customer: {_id}, {_name}";
        }
    }
}
