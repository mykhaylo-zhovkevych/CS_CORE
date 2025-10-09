using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;

namespace ConsoleAppTest5._4
{

    public class TestUser : User
    {
        public TestUser(Guid id, string name) : base(id, name) { }

        public override int LoanPeriod { get; set; }
    }

    [TestClass]
    public sealed class LibraryTest
    {
        private static ConsoleApp5._4.Library _library;
        private static User _student;
        private static User _teacher;
        private static TestUser _testUser;
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
            _testUser = new TestUser(Guid.NewGuid(), "TestUserOne");

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
        public void BorrowItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, "TestBookOne");

            // Assert
            Assert.AreEqual(_book.IsBorrowed, true);
        }

        [TestMethod]
        public void BorrowItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.BorrowItem(null, "TestBookOne");
            var returnResult = _library.ReturnItem(_student, "TestBookOne");

            // Act
            _library.BorrowItem(null, "TestBookOne");

            // Assert
            Assert.AreEqual(_book.IsBorrowed, result.Success);
            Assert.IsFalse(returnResult.Success);
        }

        [TestMethod]
        public void BorrowItem_When_ReservedIs()
        {
            // Arrange
            _videoGame.IsBorrowed = true;

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _library.BorrowItem(_teacher, "TestVideoGameOne"));
        }

        [TestMethod]
        public void BorrowItem_When_NonExistingPolicyException()
        {
            // Act & Assert
            Assert.ThrowsException<NonExistingPolicyException>(() => _library.BorrowItem(_testUser, "TestBookOne"));
        }



        [TestMethod]
        public void ReturnItem_When_DataIsCorrect()
        {

            // Act
            _library.BorrowItem(_student, "TestBookOne");

            _library.ReturnItem(_student, "TestBookOne");

            // Assert
            Assert.AreEqual(false, _book.IsBorrowed);
        }


        [TestMethod]
        public void ReturnItem_When_DataIsWrong()
        {

            // Assert & Act
            

        }


        [TestMethod] 
        public void ReturnItem_Is_EvenCalled()
        {

        }


        [TestMethod]
        public void ReserveItem()
        {
            // Arrange 
            // Act
            // Assert
        }

        [TestMethod]
        public void CancleReservation()
        {
            // Arrange 
            // Act
            // Assert
        }

        [TestMethod]
        public void ExtendBorrowingPeriod()
        {
            // Arrange 
            // Act
            // Assert
        }

        [TestMethod]
        public void ShowActiveBorrowings()
        {
            // Arrange 
            // Act
            // Assert
        }

        [TestMethod]
        public void ShowInactiveBorrowings()
        {
            // Arrange 
            // Act
            // Assert
        }


        [TestMethod]
        public void ShowVideoGameWithSpecificAgeRatingInLibrary()
        {
            // Arrange 
            // Act
            // Assert
        }


        [TestMethod]
        public void CountAllBooksInLibrary()
        {
            // Arrange 
            // Act
            // Assert
        }

        [TestMethod]
        public void ShowAllItemsWithSameName()
        {
            // Arrange 
            // Act
            // Assert
        }

    }
}
