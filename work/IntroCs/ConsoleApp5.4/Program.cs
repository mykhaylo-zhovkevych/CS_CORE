using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static ConsoleApp5._4.Library;
using System.Linq.Expressions;

namespace ConsoleApp5._4
{
    public class Program
    {
        static void Main(string[] args)
        {
            var policyProvider = new DefaultBorrowPolicyProvider();

            var library = new Library("Central Library", "123 Main St.", policyProvider);
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            var studnet = new Student(Guid.NewGuid(), "TestName01");
            var teacher = new Teacher(Guid.NewGuid(), "TestName02");
            var admin = new Admin(Guid.NewGuid(), "TestAdmin");

            var book = new Book(Guid.NewGuid(), "TestNameBook", "TestPublisher");
            var book02 = new Book(Guid.NewGuid(), "TestNameBook02", "TestPublisher");
            var videoGame = new VideoGame(Guid.NewGuid(), "TestNameGame", GameType.RPG, 19);

            shelf1.AddItemToShelf(book);
            shelf1.AddItemToShelf(book02);
            shelf1.AddItemToShelf(videoGame);

            library.AddShelf(shelf1);
            library.AddShelf(shelf2);

            library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };


            Console.WriteLine(DebuggerPrinter.PrintOutput(library.ReserveItem(studnet, "TestNameBook")));


            //Console.WriteLine(library.BorrowItem(teacher, "TestNameBook"));

            //Console.WriteLine(library.ExtendBorrowingPeriod(teacher, "TestNameBook"));
            //Console.WriteLine(library.ExtendBorrowingPeriod(teacher, "TestNameBook"));
            //Console.WriteLine(library.ExtendBorrowingPeriod(teacher, "TestNameBook"));
            //Console.WriteLine(library.ExtendBorrowingPeriod(teacher, "TestNameBook"));
            //Console.WriteLine(library.ExtendBorrowingPeriod(teacher, "TestNameBook"));

            //Console.WriteLine(library.BorrowItem(teacher, "TestNameBook"));
            //Console.WriteLine(library.ReturnItem(teacher, "TestNameBook"));


            var results1 = library.QueryItems(nameContains: "TestNameBook", isBorrowed: false, itemType: typeof(Book));

            var results2 = library.QueryItems(isReserved: true);
 
            var results3 = library.QueryItems(customPredicate: i => (i is VideoGame vg) && vg.AgeRating < 18);


            foreach (var item in results1)
            {
                Console.WriteLine(item);
            }


            //Console.WriteLine(DebuggerPrinter.PrintOutput(library.BorrowItem(studnet, "TestNameBook")));

            //try 
            //{
            //    Console.WriteLine(DebuggerPrinter.PrintOutput(library.BorrowItem(studnet, "TestNameBoffok")));
            //} catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Console.WriteLine(DebuggerPrinter.PrintOutput(library.ReserveItem(teacher, "TestNameBook")));

            //Console.WriteLine(DebuggerPrinter.PrintOutput(library.ExtendBorrowingPeriod(studnet, "TestNameBook")));
            //Console.WriteLine(DebuggerPrinter.PrintOutput(library.ReturnItem(studnet, "TestNameBook")));

            // No changes: because 
            // I might want to decide at runtime which method to call based on some condition
            /*
                performPrintOut printer;

                if (studnet.HasActiveBorrowings)
                    printer = library.ShowActiveBorrowings;
                else 
                    printer = library.ShowInactiveBorrowings;
                Console.WriteLine(printer(studnet));
             */
            PerformPrintOutput active = library.ShowActiveBorrowings;
            PerformPrintOutput nonActive = library.ShowInactiveBorrowings;

            Console.WriteLine("-------------------------");
            Console.WriteLine(active(studnet));
            Console.WriteLine(nonActive(studnet));



        }
    }
}
