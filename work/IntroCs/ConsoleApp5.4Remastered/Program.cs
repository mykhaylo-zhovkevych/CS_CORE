using ConsoleApp5._4Remastered.Data;

namespace ConsoleApp5._4Remastered
{
    public class Program
    {
        static void Main(string[] args)
        {

            var library = new Library("Central Library", "123 Main St.");
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);
            
            var studnet = new User("testname01", Enum.UserType.Student);
            var teacher = new User("testname02", Enum.UserType.Teacher);
            var teacher2 = new User("testname03", Enum.UserType.Teacher);

            var book = new Item("TestNameBook", Enum.ItemType.Book);
            var boardGame = new Item("TestNameBoardGame", Enum.ItemType.BoardGame);


            shelf1.AddItemToShelf(book);
            shelf1.AddItemToShelf(boardGame);

            library.AddShelf(shelf1);
            library.AddShelf(shelf2);

            var defaultPolicy = new Policy
            {
                PolicyName = "Default teacher book policy",
                User = teacher2,
                Item = book,
            };

            defaultPolicy.SetValues(extensions: 2, loanFees: 0.5m, loanPeriod: 14);
            PolicyService.AddPolicy(defaultPolicy);


            library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };

            //PolicyService.Policies.Clear();
            Console.WriteLine(library.BorrowItem(studnet, book));
            
            Console.WriteLine(library.ReserveItem(teacher2, book));


            IEnumerable<Item> result01 = library.QueryItems(nameContains: "TestNameBook", isBorrowed: true, itemType: Enum.ItemType.Book);
            var firstItem = result01.FirstOrDefault();


            //Console.WriteLine(result01.First());
            Console.WriteLine(library.ReserveItem(teacher2, firstItem));

        }
    }
}
