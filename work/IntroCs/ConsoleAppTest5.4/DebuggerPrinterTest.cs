using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest5._4
{
    [TestClass]
    public class DebuggerPrinterTest
    {
        private static ConsoleApp5._4.Library _library;
        private static User _student;
        private static User _teacher;
        private static Item _book;
        private static Item _videoGame;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            var policyProvider = new DefaultBorrowPolicyProvider();

            _library = new ConsoleApp5._4.Library("Central Library", "123 Main St.", policyProvider);
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            _student = new Student(Guid.NewGuid(), "TestStudentOne");
            _teacher = new Teacher(Guid.NewGuid(), "TestTeacherOne");

            _book = new Book(Guid.NewGuid(), "TestBookOne", "TestPublisherOne");
            _videoGame = new VideoGame(Guid.NewGuid(), "TestVideoGameOne", GameType.RPG, 20);

            shelf1.AddItemToShelf(_book);
            shelf1.AddItemToShelf(_videoGame);

            _library.AddShelf(shelf1);
            _library.AddShelf(shelf2);

            _library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };
        }


        [TestMethod]
        public void PrintOutput_With_DifferentTypes()
        {
            // Arrange
            string output01;
            string output02;


            // Act
            var result01 = _library.BorrowItem(_teacher, "TestBookOne");

            output01 = DebuggerPrinter.PrintOutput(result01);


            var result02 = _library.ReserveItem(_teacher, "TestBookOne");

            output02 = DebuggerPrinter.PrintOutput(result02);



            // Assert

            // disputable
            Assert.IsInstanceOfType(result01.Data, typeof(Borrowing));
            Assert.IsInstanceOfType(result02.Data, typeof(Item));

            // why
            // StringAssert.Equals(output01, output02);

        }

        [TestMethod]
        public void PrintOutput_When_NoDataFound()
        {
            // Arrange
            Result<Borrowing> result01;

            string expectedOutput = @"[FALSE] Item name is missing";
            string actualOutput;

            // Act
            result01 = _library.BorrowItem(_teacher, null);

            actualOutput = DebuggerPrinter.PrintOutput(result01);


            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void PrintOutput_When_DataFound()
        {
            // Arrange
            Result<Borrowing> result02;

            var now = DateTime.Now;

            string expectedOutput = $@"[CORRECT]User: TestStudentOne, Item: TestVideoGameOne, LoanDate: {now}, DueDate: {now.AddDays(21)}, Returned Date: , Returned: False
Saved";
            string actualOutput;

            // Act
            result02 = _library.BorrowItem(_student, "TestVideoGameOne");

            actualOutput = DebuggerPrinter.PrintOutput(result02);


            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);

        }
    }
}