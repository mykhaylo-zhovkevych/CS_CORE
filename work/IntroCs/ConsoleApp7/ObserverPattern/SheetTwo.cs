namespace ObserverPattern
{ 

    public class SheetTwo : Observer
    {
        private int _total;

        private DataSource _dataSource;

        // Warum übergebe ich hier die DataSource? 
        public SheetTwo(DataSource dataSource)
        {
            _dataSource = dataSource;
        }


        private int GetTotal()
        {
            return _total;
        }


        public void Update()
        {
            _total = CalculateTotal(_dataSource.GetValues());
        }


        public int CalculateTotal(List<int> values)
        {
            var sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }
            Console.WriteLine("Total: "+ sum);
            return sum;
        }

  
    }

}