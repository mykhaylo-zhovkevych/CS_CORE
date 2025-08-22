namespace ConsoleApp2._3
{
    class Program
    {

    static void Main(string[] args)
    {
        Console.WriteLine("=== WAREHOUSE AUTOMATION TEST ===\n");

        var warehouse = new Warehouse("Haupt", "Zürich");
        var cell1 = new Cell(1);
        var cell2 = new Cell(2);
        var cell3 = new Cell(3);

        var laptop = new Product(101, "Laptop", 10);
        var smartphone = new Product(102, "Smartphone", 20);
        var tablet = new Product(103, "Tablet", 15);

        cell1.StoreProduct(laptop);
        cell1.StoreProduct(smartphone);
        cell2.StoreProduct(tablet);
        cell1.StoreProduct(tablet);
        cell3.StoreProduct(smartphone);


        var wagon = new AutomaticWagon(1000);
        Console.WriteLine($"Wagon {wagon.WagonNumber} erstellt\n");

        Console.WriteLine("=== TEST 1: Einfacher Transport ===");
        var order1 = new TransportOrder(201, 3, cell1, cell3, laptop);

        var order4 = new TransportOrder(204, 3, cell1, cell2, laptop);
        var order5 = new TransportOrder(205, 3, cell3, cell1, laptop);

        Console.WriteLine($"Order {order1.TranportOrderNumber}: {order1.Quantity}x {order1.Product.Name} von Cell {order1.SourceCell.CellNumber} zu Cell {order1.TargetCell.CellNumber}\n");

        wagon.ProcessOrders(order1);
        Console.WriteLine("\n");
        wagon.ProcessOrders(order4);
        Console.WriteLine("\n");
        wagon.ProcessOrders(order5);


        Console.WriteLine("=== TEST 2: Mehrere Aufträge nacheinander ===");
        var order2 = new TransportOrder(202, 4, cell1, cell2, tablet);
        var order3 = new TransportOrder(203, 5, cell2, cell3, tablet);

        Console.WriteLine($"Order {order2.TranportOrderNumber}: {order2.Quantity}x {order2.Product.Name} von Cell {order2.SourceCell.CellNumber} zu Cell {order2.TargetCell.CellNumber}\n");
        wagon.LoadOrder(order2);
        wagon.UnloadOrder(order2);

        Console.WriteLine($"Order {order3.TranportOrderNumber}: {order3.Quantity}x {order3.Product.Name} von Cell {order3.SourceCell.CellNumber} zu Cell {order3.TargetCell.CellNumber}\n");
        wagon.LoadOrder(order3);
        wagon.UnloadOrder(order3);


            Console.WriteLine("\n=== TEST 3: Error-Fall ===");
            try
            {
                var invalidOrder = new TransportOrder(206, 999, cell1, cell2, laptop);
                wagon.LoadOrder(invalidOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erwarteter Fehler gefangen: {ex.Message}");
            }
        }
    }
}