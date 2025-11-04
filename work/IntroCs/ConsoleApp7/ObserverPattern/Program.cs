namespace ObserverPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSource dataSource = new DataSource();

            SheetTwo sheet2 = new SheetTwo(dataSource);

            BarChart barChart = new BarChart(dataSource);

            dataSource.AddObserver(sheet2);
            dataSource.AddObserver(barChart);

            dataSource.SetValues(new List<int> { 10, 20, 30 });
            dataSource.SetValues(new List<int> { 20, 10, 10 });
        }
    }
}
