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
                User = teacher,
                Item = book,
            };

            defaultPolicy.SetValues(extensions: 2, loanFees: 0.5m, loanPeriod: 14);

            PolicyService.AddPolicy(defaultPolicy);



            library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };

            // Why dosen't it cast into List? 
            IEnumerable<Item> result01 = library.QueryItems(nameContains: "TestNameBook", isBorrowed: true, itemType: Enum.ItemType.Book);



            //Logic error 

            Console.WriteLine(library.BorrowItem(teacher2, book));
            // here same user type but different user
            // not exsisting policy for user? no
            // create a new policy for user? no
            // because if yes than a for each user must a new policy be created
            // solution not policy create for user but for user type, because it doenst matter which user particular(instance) it is 



            Console.WriteLine(result01.First());
            


        }
    }
}
