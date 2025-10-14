using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;

namespace ConsoleAppTest5._4
{
    [TestClass]
    public class DebuggerPrinterTest
    {
        private Library _library;
        private User _student;
        private User _teacher;
        private Item _book;
        private Item _videoGame;

        [TestInitialize]
        public void ClassInit()
        {
            var policyProvider = new DefaultBorrowPolicyProvider();

            _library = new Library("Central Library", "123 Main St.", policyProvider);
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            _student = new Student(Guid.NewGuid(), "TestStudentOne");
            _teacher = new Teacher(Guid.NewGuid(), "TestTeacherOne");

            _book = new Book("TestBookOne", "TestPublisherOne");
            _videoGame = new VideoGame("TestVideoGameOne", GameType.RPG, 20);

            shelf1.AddItemToShelf(_book);
            shelf1.AddItemToShelf(_videoGame);

            _library.AddShelf(shelf1);
            _library.AddShelf(shelf2);

            _library.InformReserver += (sender, e) =>
            {
                Console.WriteLine($"Notification: Item '{e.Item.Name}' is now available for {e.ReservedUser?.Name}");
            };
        }

        // Just data with different types to test,and the instances 
        [TestMethod]
        public void PrintOutput_With_DifferentTypes()
        {
            // Arrange
            string output01;
            string output02;


            // Act
            Result<Borrowing> result01 = (Result<Borrowing>)_library.BorrowItem(_teacher, "TestBookOne");
            output01 = DebuggerPrinter.PrintOutput(result01);


            Result result02 = _library.ReserveItem(_teacher, "TestBookOne");
            output02 = DebuggerPrinter.PrintOutput(result02);

            // Assert
            Assert.IsInstanceOfType(result01.Data, typeof(Borrowing));   


            // why
            // StringAssert.Equals(output01, output02);

        }

        [TestMethod]
        public void PrintOutput_When_NoDataFound()
        {
            // Arrange

            string expectedOutput = @"[FALSE] Item name is missing";
            string actualOutput;

            // Act
            Result result01 = _library.BorrowItem(_teacher, null);

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

            string expectedOutput =
    $@"[CORRECT]User: TestStudentOne, Item: TestVideoGameOne, LoanDate: {now}, DueDate: {now.AddDays(21)}, Returned Date: , Returned: False
Saved
";
            string actualOutput;

            // Act
            result02 = (Result<Borrowing>)_library.BorrowItem(_student, "TestVideoGameOne");

            actualOutput = DebuggerPrinter.PrintOutput(result02);


            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);

        }
    }
}