using ConsoleApp5._2;
using ConsoleApp5._2.Objects;
using ConsoleApp5._2.Users;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using System.Diagnostics.CodeAnalysis;

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

            
            var studnet = new Student(Guid.NewGuid(),"TestName01");
            var teacher = new Teacher(Guid.NewGuid(), "TestName02");
            var book = new Book(Guid.NewGuid(),"TestNameBook", "TestPublisher");
            var videoGame = new VideoGame(Guid.NewGuid(), "TestNameGame", GameType.SIMULATOR, 18);

            shelf1.AddItemToShelf(book);
            shelf1.AddItemToShelf(videoGame);
            shelf2.AddItemToShelf(videoGame);

            library.AddShelf(shelf1);
            library.AddShelf(shelf2);

            library.BorrowItem(studnet, "TestNameBook");

            // library.ReserveItem(teacher, "TestNameGame");

            foreach (var b in library.GetBorrowings())
            {
                Console.WriteLine(b);
            }

        }
    }
}
