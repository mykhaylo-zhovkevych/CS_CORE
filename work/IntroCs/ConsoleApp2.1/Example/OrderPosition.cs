namespace ConsoleApp2._1.Example
{
    class OrderPosition
    {
        private int _amount;
        private string _productName;
        private float _unitPrice;

        public OrderPosition(int amount, string productName, float unitPrice)
        {
            _amount = amount;
            _productName = productName;
            _unitPrice = unitPrice;
        }

        public float CalculatePositionPrice()
        {
            return _amount * _unitPrice;
        }

        public override string ToString()
        {
            return $"{_productName}, x {_amount} = {CalculatePositionPrice():0.00} EUR";
        }
    }
}
