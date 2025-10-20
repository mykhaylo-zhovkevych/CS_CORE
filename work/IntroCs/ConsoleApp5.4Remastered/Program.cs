using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Exceptions;

namespace ConsoleApp5._4Remastered
{
    public class Program
    {
        static void Main(string[] args)
        {

            var library = new Library("Central Library", "123 Main St.");
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            var studnet = new User("testname01", UserType.Student);
            var teacher = new User("testname02", UserType.Teacher);
            var teacher2 = new User("testname03", UserType.Teacher);

            var book = new Item("TestNameBook", ItemType.Book);
            var boardGame = new Item("TestNameBoardGame", ItemType.BoardGame);


            shelf1.AddItemToShelf(book);
            shelf1.AddItemToShelf(boardGame);

            library.AddShelf(shelf1);
            library.AddShelf(shelf2);

            var defaultPolicy = new Policy
            {
                PolicyName = "Default teacher book policy",

            };

            defaultPolicy.SetValues(extensions: 2, loanFees: 0.5m, loanPeriodInDays: 30);
            PolicyService.AddPolicy(UserType.Teacher, ItemType.Book, defaultPolicy);


            library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };

            //PolicyService.Policies.Clear();

            try 
            { 
                Console.WriteLine(library.BorrowItem(teacher, book)); 
            }
            catch (IsAlreadyBorrowedException ex)
            {
                Console.WriteLine($"Cannot borrow item: {ex.Message}");
            }
            catch (NonExistingPolicyException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            try
            {
                Console.WriteLine(library.ReserveItem(studnet, book));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            //try 
            //{ 
            //    Console.WriteLine(library.ExtendBorrowingPeriod(teacher, book)); 
            //}
            //catch (IsAlreadyBorrowedException ex)
            //{
            //    Console.WriteLine($"Cannot borrow item: {ex.Message}");
            //}
            //catch (NonExistingPolicyException ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}
            //catch (ArgumentException ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}


            try
            {
                Console.WriteLine(library.ReturnItem(teacher, book));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            //try
            //{
            //    Console.WriteLine(library.CancelReservation(teacher, book));
            //}
            //catch (ArgumentException ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}


            //try
            //{
            //    Console.WriteLine(library.ExtendBorrowingPeriod(teacher, book));
            //}
            //catch (ArgumentException ex)
            //{
            //    Console.WriteLine($"Unexpected error: {ex.Message}");
            //    // The program will re-throw the exception to be handled by the caller 
            //    throw;
            //}

            //IEnumerable<Item> result01 = library.QueryItems(nameContains: "TestNameBook", isBorrowed: true, itemType: Enum.ItemType.Book);
            //var firstItem = result01.FirstOrDefault();


            ////Console.WriteLine(result01.First());
            //Console.WriteLine(library.ReserveItem(teacher2, firstItem));

        }
    }
}
