namespace CompositionPattern
{
    internal class Microphone : IItems
    {

        private float _price = 25.00f;

        public float GetPrice()
        {
            return _price;
        }

    }

}