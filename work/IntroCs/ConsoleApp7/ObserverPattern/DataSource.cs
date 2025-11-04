namespace ObserverPattern
{ 

    public class DataSource : Subject
    {
        private List<int> _values = new List<int>();

        public List<int> GetValues()
        {
            return _values;
        }

        public void SetValues(List<int> values)
        {
            _values = values;

            NotifyObservers();

            //foreach (var dependent in _dependents)
            //{
            //    if(dependent is SheetTwo)
            //    {
            //        (dependent as SheetTwo).CalculateTotal(_values);
            //    } else if (dependent is BarChart)
            //    {
            //        (dependent as BarChart).Render(_values);
            //    }
            //}
        }

        // This class dont manages the observes
        //public void AddDependent(Object dependent)
        //{
        //    _dependents.Add(dependent);
        //}
        //public void RemoveDependent(Object dependent)
        //{
        //    _dependents.Remove(dependent);
        //}

    }
}