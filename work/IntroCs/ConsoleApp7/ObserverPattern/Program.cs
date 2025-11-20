namespace ObserverPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSource dataSource = new DataSource();

            Observer observer1 = new SheetTwo(dataSource);

            Observer observer2 = new BarChart(dataSource);

            dataSource.AddObserver(observer1);
            dataSource.AddObserver(observer2);

            dataSource.SetValues(new List<int> { 10, 20, 30 });
            dataSource.SetValues(new List<int> { 20, 10, 10 });
        }
    }
}
