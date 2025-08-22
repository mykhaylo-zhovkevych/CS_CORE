using System;
using System.Collections.Generic;

namespace ConsoleApp2._1.Example
{
    class Order
    {
        private int _nr;
        private DateTime _date;
        private List<OrderPosition> _positions = new List<OrderPosition>();

        public Order(int nr, DateTime date)
        {
            _nr = nr;
            _date = date;
        }

        public void AddPosition(OrderPosition pos)
        {
            _positions.Add(pos);
        }

        public float CalculatePrice()
        {
            float total = 0;
            foreach (var pos in _positions)
            {
                total += pos.CalculatePositionPrice();
            }
            return total;
        }

        public override string ToString()
        {
            string positionsInfo = string.Join("\n   ", _positions);
            return $"Order #{_nr} ({_date:d}):\n   {positionsInfo}\n   Total: {CalculatePrice():0.00} EUR";
        }
    }
}
